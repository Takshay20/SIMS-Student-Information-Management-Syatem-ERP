using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using SIMS_Dapper.Models;
using SIMS_Dapper.Models.ViewModels;

namespace SIMS_Dapper.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection => new SqlConnection(
                    _configuration.GetConnectionString("DBCS"));

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var db = Connection)
            {
                var user = db.QueryFirstOrDefault<LoginResult>(
                    "SP_UserLogin",
                    new
                    {
                        Username = model.Username,
                        Password = model.Password
                    },
                    commandType: CommandType.StoredProcedure
                );

                if (user == null)
                {
                    ViewBag.Error = "Invalid username or password";
                    return View(model);
                }

                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("RoleName", user.RoleName);

                switch (user.RoleName)
                {
                    case "Admin":
                        return RedirectToAction("Dashboard", "Admin");

                    case "HOD":
                        return RedirectToAction("Dashboard", "Admin");

                    case "Professor Incharge":
                        return RedirectToAction("Dashboard", "Admin");

                    case "Class Coordinator":
                        return RedirectToAction("Dashboard", "Admin");

                    case "Faculty":
                        return RedirectToAction("Index", "Faculty");

                    case "Student":
                        return RedirectToAction("Index", "Student");

                    case "Parent":
                        return RedirectToAction("AccessDenied");

                    case "Dean":
                        return RedirectToAction("Dashboard", "Admin");

                    default:
                        return RedirectToAction("Login");
                }
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}