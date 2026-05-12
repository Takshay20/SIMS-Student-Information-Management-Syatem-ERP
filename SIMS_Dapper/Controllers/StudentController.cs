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
            using (var db = Connection)
            {
                   db.Execute(
                    "SP_InsertStudents",
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
    }
}