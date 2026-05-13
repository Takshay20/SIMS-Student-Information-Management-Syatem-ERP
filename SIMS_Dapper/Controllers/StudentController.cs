using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using SIMS_Dapper.Models;

namespace SIMS_Dapper.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(
                    _configuration.GetConnectionString("DBCS"));
            }
        }

        public IActionResult Index()
        {
            using (var db = Connection)
            {
                var students = db.Query<Student>(
                    "SP_GetAllStudents",
                    commandType: CommandType.StoredProcedure
                );

                return View(students);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (student.ParentId == 0)
            {
                student.ParentId = null;
            }

            using (var db = Connection)
            {
                db.Execute(
                    "sp_InsertStudent",
                    new
                    {
                        student.UserId,
                        student.ParentId,
                        student.StudentName,
                        student.FatherName,
                        student.MotherName,
                        student.EnrollmentNo,
                        student.RollNo,
                        student.Email,
                        student.Phone,
                        student.Gender,
                        student.DOB,
                        student.CourseId,
                        student.BranchId,
                        student.SectionId,
                        student.Semester,
                        student.Session,
                        student.Address
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (var db = Connection)
            {
                var student = db.QueryFirstOrDefault<Student>(
                    "sp_GetStudentById",
                    new { StudentId = id },
                    commandType: CommandType.StoredProcedure
                );

                return View("Create", student);
            }
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (student.ParentId == 0)
            {
                student.ParentId = null;
            }

            using (var db = Connection)
            {
                db.Execute(
                    "sp_UpdateStudent",
                    new
                    {
                        student.StudentId,
                        student.UserId,
                        student.ParentId,
                        student.StudentName,
                        student.FatherName,
                        student.MotherName,
                        student.EnrollmentNo,
                        student.RollNo,
                        student.Email,
                        student.Phone,
                        student.Gender,
                        student.DOB,
                        student.CourseId,
                        student.BranchId,
                        student.SectionId,
                        student.Semester,
                        student.Session,
                        student.Address
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            using (var db = Connection)
            {
                db.Execute(
                    "sp_DeleteStudent",
                    new { StudentId = id },
                    commandType: CommandType.StoredProcedure
                );
            }

            return RedirectToAction("Index");
        }
    }
}