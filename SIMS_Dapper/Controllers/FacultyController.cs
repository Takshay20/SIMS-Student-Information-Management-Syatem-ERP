using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIMS_Dapper.Models;
using SIMS_Dapper.Models.ViewModels;
using SIMS_Dapper.Filters;

namespace SIMS_Dapper.Controllers
{
    [RoleAuthorize("Admin", "HOD", "Faculty")]
    public class FacultyController : Controller
    {
        private readonly IConfiguration _configuration;

        public FacultyController(IConfiguration configuration)
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
                var faculty = db.Query<Faculty>(
                    "sp_GetAllFaculty",
                    commandType: CommandType.StoredProcedure
                );

                return View(faculty);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            using (var db = Connection)
            {
                var users = db.Query(
                    "sp_GetFacultyUsers",
                    commandType: CommandType.StoredProcedure
                );

                var departments = db.Query(
                    "sp_GetDepartments",
                    commandType: CommandType.StoredProcedure
                );

                var model = new FacultyFormViewModel
                {
                    Faculty = new Faculty(),

                    Users = users.Select(x => new SelectListItem
                    {
                        Value = x.UserId.ToString(),
                        Text = x.Username
                    }).ToList(),

                    Departments = departments.Select(x => new SelectListItem
                    {
                        Value = x.DepartmentId.ToString(),
                        Text = x.DepartmentName
                    }).ToList()
                };

                return View("Form", model);
            }
        }

        [HttpPost]
        public IActionResult Create(FacultyFormViewModel model)
        {
            using (var db = Connection)
            {
                db.Execute(
                    "sp_InsertFaculty",
                    new
                    {
                        model.Faculty.UserId,
                        model.Faculty.FacultyName,
                        model.Faculty.EmployeeCode,
                        model.Faculty.Email,
                        model.Faculty.Phone,
                        model.Faculty.DepartmentId,
                        model.Faculty.Designation
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
                var faculty = db.QueryFirstOrDefault<Faculty>(
                    "sp_GetFacultyById",
                    new { FacultyId = id },
                    commandType: CommandType.StoredProcedure
                );

                var users = db.Query(
                    "sp_GetFacultyUsers",
                    commandType: CommandType.StoredProcedure
                );

                var departments = db.Query(
                    "sp_GetDepartments",
                    commandType: CommandType.StoredProcedure
                );

                var model = new FacultyFormViewModel
                {
                    Faculty = faculty,

                    Users = users.Select(x => new SelectListItem
                    {
                        Value = x.UserId.ToString(),
                        Text = x.Username
                    }).ToList(),

                    Departments = departments.Select(x => new SelectListItem
                    {
                        Value = x.DepartmentId.ToString(),
                        Text = x.DepartmentName
                    }).ToList()
                };

                return View("Form", model);
            }
        }

        [HttpPost]
        public IActionResult Edit(FacultyFormViewModel model)
        {
            using (var db = Connection)
            {
                db.Execute(
                    "sp_UpdateFaculty",
                    new
                    {
                        model.Faculty.FacultyId,
                        model.Faculty.UserId,
                        model.Faculty.FacultyName,
                        model.Faculty.EmployeeCode,
                        model.Faculty.Email,
                        model.Faculty.Phone,
                        model.Faculty.DepartmentId,
                        model.Faculty.Designation
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
                    "sp_DeleteFaculty",
                    new { FacultyId = id },
                    commandType: CommandType.StoredProcedure
                );
            }

            return RedirectToAction("Index");
        }
    }
}