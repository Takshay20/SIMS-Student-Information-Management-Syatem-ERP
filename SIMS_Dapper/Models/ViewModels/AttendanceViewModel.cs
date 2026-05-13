using Microsoft.AspNetCore.Mvc.Rendering;

namespace SIMS_Dapper.Models.ViewModels
{
    public class AttendanceViewModel
    {
        public AttendanceSession Session { get; set; } = new();

        public List<SelectListItem> Subjects { get; set; } = new();

        public List<SelectListItem> Sections { get; set; } = new();

        public List<AttendanceRecord> Students { get; set; } = new();
    }
}