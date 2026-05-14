using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using SIMS_Dapper.Models;
using SIMS_Dapper.Filters;

namespace SIMS_Dapper.Controllers
{

    [RoleAuthorize("Admin", "HOD", "Professor Incharge", "Class Coordinator")]
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
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



        public IActionResult Dashboard()
        {
            using (var db = Connection)
            {
                var stats = db.QueryFirstOrDefault<DashboardStats>(
                    "SP_AdminDashboardStats",
                    commandType: CommandType.StoredProcedure
                );

                return View(stats);
            }
        }
    }
}