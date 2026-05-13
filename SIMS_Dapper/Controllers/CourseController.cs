using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using SIMS_Dapper.Models;

namespace SIMS_Dapper.Controllers
{
    public class CourseController : Controller
    {
        private readonly IConfiguration _configuration;

        public CourseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection =>
            new SqlConnection(_configuration.GetConnectionString("DBCS"));

        public IActionResult Index()
        {
            using var db = Connection;

            var courses = db.Query<Course>(
                "sp_GetAllCourses",
                commandType: CommandType.StoredProcedure
            );

            return View("CourseList",courses);
        }
    }
}