using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using SIMS_Dapper.Filters;
using SIMS_Dapper.Models;
using SIMS_Dapper.Models.ViewModels;

namespace SIMS_Dapper.Controllers
{
    [RoleAuthorize("Faculty", "HOD")]
    public class ExamController : Controller
    {
        private readonly IConfiguration _configuration;

        public ExamController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection =>
            new SqlConnection(_configuration.GetConnectionString("DBCS"));

        [HttpGet]
        public IActionResult ExamSchedule()
        {
            using var db = Connection;

            var courses = db.Query<Course>(
                "sp_GetAllCourses",
                commandType: CommandType.StoredProcedure
            );

            var branches = db.Query<Branch>(
                "sp_GetAllBranches",
                commandType: CommandType.StoredProcedure
            );

            var subjects = db.Query<Subject>(
                "sp_GetAllSubjects",
                commandType: CommandType.StoredProcedure
            );

            var exams = db.Query<ExamSchedule>(
                "sp_GetAllExamSchedules",
                commandType: CommandType.StoredProcedure
            );

            var model = new ExamViewModel
            {
                Courses = courses.Select(x => new SelectListItem
                {
                    Value = x.CourseId.ToString(),
                    Text = x.CourseName
                }).ToList(),

                Branches = branches.Select(x => new SelectListItem
                {
                    Value = x.BranchId.ToString(),
                    Text = x.BranchName
                }).ToList(),

                Subjects = subjects.Select(x => new SelectListItem
                {
                    Value = x.SubjectId.ToString(),
                    Text = x.SubjectName
                }).ToList(),

                Exams = exams.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveExam(ExamViewModel model)
        {
            using var db = Connection;

            db.Execute(
                "sp_InsertExamSchedule",
                new
                {
                    model.Exam.CourseId,
                    model.Exam.BranchId,
                    model.Exam.Semester,
                    model.Exam.SubjectId,
                    model.Exam.ExamType,
                    model.Exam.ExamDate,
                    model.Exam.StartTime,
                    model.Exam.EndTime,
                    model.Exam.Venue
                },
                commandType: CommandType.StoredProcedure
            );

            TempData["SuccessMessage"] = "Exam schedule created successfully.";

            return RedirectToAction("ExamSchedule");
        }
    }
}