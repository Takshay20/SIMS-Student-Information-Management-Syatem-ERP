using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SIMS_Dapper.Filters;
using SIMS_Dapper.Models;
using System.Data;

namespace SIMS_Dapper.Controllers
{
    [RoleAuthorize("Admin", "HOD")]
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