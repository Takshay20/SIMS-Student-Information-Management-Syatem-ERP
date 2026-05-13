using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SIMS_Dapper.Filters;
using SIMS_Dapper.Models;
using System.Data;

namespace SIMS_Dapper.Controllers
{
    [RoleAuthorize("Admin", "HOD")]
    public class BranchController : Controller
    {
        private readonly IConfiguration _configuration;

        public BranchController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection =>
            new SqlConnection(_configuration.GetConnectionString("DBCS"));

        public IActionResult Index()
        {
            using var db = Connection;

            var branches = db.Query<Branch>(
                "sp_GetAllBranches",
                commandType: CommandType.StoredProcedure
            );

            return View("BranchList",branches);
        }
    }
}