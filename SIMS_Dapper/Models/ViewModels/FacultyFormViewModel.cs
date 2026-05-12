using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;

namespace SIMS_Dapper.Models.ViewModels
{
    public class FacultyFormViewModel
    {
        public Faculty Faculty { get; set; }

        public List<SelectListItem> Users { get; set; }

        public List<SelectListItem> Departments { get; set; }
    }
}