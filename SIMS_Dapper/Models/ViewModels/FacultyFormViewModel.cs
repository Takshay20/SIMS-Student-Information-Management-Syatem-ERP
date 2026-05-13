using Microsoft.AspNetCore.Mvc.Rendering;

namespace SIMS_Dapper.Models.ViewModels
{
    public class FacultyFormViewModel
    {
        public Faculty Faculty { get; set; } = new Faculty();

        public List<SelectListItem> Users { get; set; } = new();

        public List<SelectListItem> Departments { get; set; } = new();
    }
}