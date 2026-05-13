using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIMS_Dapper.Models;
using SIMS_Dapper.Models.ViewModels;

namespace SIMS_Dapper.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IConfiguration _configuration;

        public AttendanceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection =>
            new SqlConnection(_configuration.GetConnectionString("DBCS"));

        [HttpGet]
        public IActionResult AttendanceEntry()
        {
            using var db = Connection;

            var subjects = db.Query<dynamic>(
                "sp_GetAttendanceSubjects",
                commandType: CommandType.StoredProcedure
            );

            var sections = db.Query<dynamic>(
                "sp_GetAttendanceSections",
                commandType: CommandType.StoredProcedure
            );

            var model = new AttendanceViewModel
            {
                Subjects = subjects.Select(x => new SelectListItem
                {
                    Value = x.SubjectId.ToString(),
                    Text = x.SubjectName
                }).ToList(),

                Sections = sections.Select(x => new SelectListItem
                {
                    Value = x.SectionId.ToString(),
                    Text = x.SectionName
                }).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public JsonResult GetStudents(int sectionId)
        {
            using var db = Connection;

            var students = db.Query<AttendanceRecord>(
                "sp_GetStudentsBySection",
                new { SectionId = sectionId },
                commandType: CommandType.StoredProcedure
            );

            return Json(students);
        }

        [HttpPost]
        public IActionResult SaveAttendance(AttendanceViewModel model)
        {
            using var db = Connection;

            int facultyId = 1;

            int sessionId = db.QuerySingle<int>(
                "sp_CreateAttendanceSession",
                new
                {
                    model.Session.SubjectId,
                    model.Session.SectionId,
                    FacultyId = facultyId,
                    model.Session.SessionDate,
                    model.Session.StartTime,
                    model.Session.EndTime,
                    model.Session.TopicCovered,
                    model.Session.Remarks
                },
                commandType: CommandType.StoredProcedure
            );

            foreach (var student in model.Students)
            {
                db.Execute(
                    "sp_SaveAttendanceRecord",
                    new
                    {
                        SessionId = sessionId,
                        student.StudentId,
                        student.Status
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            TempData["SuccessMessage"] = "Attendance saved successfully.";

            return RedirectToAction("AttendanceEntry");
        }
    }
}