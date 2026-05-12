using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIMS_Dapper.Models;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace SIMS_Dapper.Controllers
{
    public class SubjectController : Controller
    {
        private readonly IConfiguration _config;
        private string connectionString;

        public SubjectController(IConfiguration config)
        {
            _config = config;
            connectionString = config.GetConnectionString("DBCS");
        }

        public IActionResult Index(int? id)
        {
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                var subjects = con.Query<Subject>(
                    "sp_GetAllSubjects",
                    commandType: CommandType.StoredProcedure
                ).ToList();

                var courses = con.Query<Course>(
                    "sp_GetAllCourses",
                    commandType: CommandType.StoredProcedure
                ).ToList();

                ViewBag.Subjects = subjects;
                ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName");

                Subject model = new Subject();

                if (id != null)
                {
                    model = con.QueryFirstOrDefault<Subject>(
                        "sp_GetSubjectById",
                        new { SubjectId = id },
                        commandType: CommandType.StoredProcedure
                    );
                }

                return View(model);
            }
        }

        [HttpGet]
        public JsonResult GetBranchesByCourse(int courseId)
        {
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                var branches = con.Query<Branch>(
                    "sp_GetBranchesByCourseId",
                    new { CourseId = courseId },
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return Json(branches);
            }
        }

        [HttpPost]
        public JsonResult Save(Subject s)
        {
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                DynamicParameters parem = new DynamicParameters();

                parem.Add("@SubjectName", s.SubjectName);
                parem.Add("@SubjectCode", s.SubjectCode);
                parem.Add("@CourseId", s.CourseId);
                parem.Add("@BranchId", s.BranchId);
                parem.Add("@Semester", s.Semester);
                parem.Add("@SubjectType", s.SubjectType);

                con.Execute(
                    "sp_InsertSubject",
                    parem,
                    commandType: CommandType.StoredProcedure
                );

                return Json(new
                {
                    success = true,
                    message = "Subject saved successfully"
                });
            }
        }

        [HttpPost]
        public JsonResult Edit(Subject s)
        {
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                DynamicParameters parem = new DynamicParameters();

                parem.Add("@SubjectId", s.SubjectId);
                parem.Add("@SubjectName", s.SubjectName);
                parem.Add("@SubjectCode", s.SubjectCode);
                parem.Add("@CourseId", s.CourseId);
                parem.Add("@BranchId", s.BranchId);
                parem.Add("@Semester", s.Semester);
                parem.Add("@SubjectType", s.SubjectType);

                con.Execute(
                    "sp_UpdateSubject",
                    parem,
                    commandType: CommandType.StoredProcedure
                );

                return Json(new
                {
                    success = true,
                    message = "Subject updated successfully"
                });
            }
        }

        [HttpPost]
        public JsonResult Delete(int subjectId)
        {
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                DynamicParameters parem = new DynamicParameters();
                parem.Add("@SubjectId", subjectId);

                int result = con.Execute(
                    "sp_DeleteSubject",
                    parem,
                    commandType: CommandType.StoredProcedure
                );

                return Json(new
                {
                    success = result > 0,
                    message = result > 0
                        ? "Subject deleted successfully"
                        : "Delete failed"
                });
            }
        }
    }
}