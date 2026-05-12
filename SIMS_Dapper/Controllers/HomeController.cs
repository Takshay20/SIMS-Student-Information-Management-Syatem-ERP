//using Microsoft.AspNetCore.Mvc;
//using SIMS_Dapper.Models;
//using System.Data;
//using System.Diagnostics;
//using Dapper;
//using Microsoft.Data.SqlClient;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace SIMS_Dapper.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;
//        private readonly IConfiguration _config;
//        private string connectionString;

//        public HomeController(ILogger<HomeController> logger, IConfiguration config)
//        {
//            _logger = logger;
//            _config = config;
//            connectionString = config.GetConnectionString("DBCS");
//        }

//        [HttpGet]
//        public JsonResult GetBranchesByCourse(int courseId)
//        {
//            using (IDbConnection con = new SqlConnection(connectionString))
//            {
//                con.Open();

//                var branches = con.Query<Branch>(
//                    "sp_GetBranchesByCourseId",
//                    new { CourseId = courseId },
//                    commandType: CommandType.StoredProcedure
//                ).ToList();

//                return Json(branches);
//            }
//        }

//        public IActionResult Index(int? id)
//        {
//            using (IDbConnection con = new SqlConnection(connectionString))
//            {
//                con.Open();

  
//                var students = con.Query<Student>(
//                    "sp_GetAllStudent",
//                    commandType: CommandType.StoredProcedure
//                ).ToList();


//                var courses = con.Query<Course>(
//                    "sp_GetAllCourses",
//                    commandType: CommandType.StoredProcedure
//                ).ToList();

//                ViewBag.Students = students;
//                ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName");

//                Student model = new Student();


//                if (id != null)
//                {
//                    model = con.QueryFirstOrDefault<Student>(
//                        "sp_GetById",
//                        new { StudentId = id },
//                        commandType: CommandType.StoredProcedure
//                    );
//                }

//                return View(model);
//            }
//        }
//        [HttpPost]
//        public IActionResult Save(Student s)
//        {
//            using (IDbConnection con = new SqlConnection(connectionString))
//            {
//                DynamicParameters parem = new DynamicParameters();
//                parem.Add("@EnrollmentNo", s.EnrollmentNo);
//                parem.Add("@RollNo", s.RollNo);
//                parem.Add("@StudentName", s.StudentName);
//                parem.Add("@FatherName", s.FatherName);
//                parem.Add("@MotherName", s.MotherName);
//                parem.Add("@StudentEmail", s.StudentEmail);
//                parem.Add("@StudentPhone", s.StudentPhone);
//                parem.Add("@Gender", s.Gender);
//                parem.Add("@CourseId", s.CourseId);
//                parem.Add("@BranchId", s.BranchId);
//                parem.Add("@Address", s.Address);
//                parem.Add("@Semester", s.Semester);
//                parem.Add("@Password", s.Password);
//                con.Execute("sp_InsertStudent", parem, commandType: CommandType.StoredProcedure);

//                return Json(new
//                {
//                    success = true,
//                    message = "Student saved successfully"
//                });
//            }
//        }
//        [HttpPost]
//        public IActionResult Edit(Student s)
//        {
//            using (IDbConnection con = new SqlConnection(connectionString))
//            {
//                DynamicParameters parem = new DynamicParameters();
//                parem.Add("@StudentId", s.StudentId);
//                parem.Add("@EnrollmentNo", s.EnrollmentNo);
//                parem.Add("@RollNo", s.RollNo);
//                parem.Add("@StudentName", s.StudentName);
//                parem.Add("@FatherName", s.FatherName);
//                parem.Add("@MotherName", s.MotherName);
//                parem.Add("@StudentEmail", s.StudentEmail);
//                parem.Add("@StudentPhone", s.StudentPhone);
//                parem.Add("@Gender", s.Gender);
//                parem.Add("@CourseId", s.CourseId);
//                parem.Add("@BranchId", s.BranchId);
//                parem.Add("@Address", s.Address);
//                parem.Add("@Semester", s.Semester);
//                parem.Add("@Password", s.Password);
//                con.Execute("sp_UpdateStudent", parem, commandType: CommandType.StoredProcedure);

//                return Json(new
//                {
//                    success = true,
//                    message = "Student updated successfully"
//                });
//            }
//        }

//        [HttpGet]
//        public IActionResult GetById(int Id)
//        {
//            using (IDbConnection con = new SqlConnection(connectionString))
//            {
//                DynamicParameters parem = new DynamicParameters();
//                parem.Add("@StudentId", Id);
//                var Student = con.QueryFirstOrDefault<Student>("sp_GetById", parem, commandType: CommandType.StoredProcedure);

//                return View(Student);
//            }
//        }

       
//        [HttpPost]
//        public JsonResult Delete(int studentId)
//        {
//            using (IDbConnection con = new SqlConnection(connectionString))
//            {
//                con.Open();

//                DynamicParameters parem = new DynamicParameters();
//                parem.Add("@StudentId", studentId);

//                int result = con.Execute(
//                    "sp_DeleteStudent",
//                    parem,
//                    commandType: CommandType.StoredProcedure
//                );

//                return Json(new
//                {
//                    success = result > 0,
//                    message = result > 0
//                        ? "Student deleted successfully"
//                        : "Delete failed"
//                });
//            }
//        }
//    }
//}
