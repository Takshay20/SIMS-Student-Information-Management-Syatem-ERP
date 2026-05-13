using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SIMS_Dapper.Filters;
using SIMS_Dapper.Models;
using System.Data;

namespace SIMS_Dapper.Controllers
{
    [RoleAuthorize("Admin", "HOD")]
    public class DepartmentController : Controller
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
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
                var departments = db.Query<Department>(
                    "sp_GetDepartments",
                    commandType: CommandType.StoredProcedure
                );

                return View(departments);
            }
        }
    }
}