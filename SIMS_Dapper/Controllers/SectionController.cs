using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using SIMS_Dapper.Models;

namespace SIMS_Dapper.Controllers
{
    public class SectionController : Controller
    {
        private readonly IConfiguration _configuration;

        public SectionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection =>
            new SqlConnection(_configuration.GetConnectionString("DBCS"));

        public IActionResult Index()
        {
            using var db = Connection;

            var sections = db.Query<Section>(
                "sp_GetAllSections",
                commandType: CommandType.StoredProcedure
            );

            return View("SectionList",sections);
        }
    }
}