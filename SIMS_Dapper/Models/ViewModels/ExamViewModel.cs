using Microsoft.AspNetCore.Mvc.Rendering;

namespace SIMS_Dapper.Models.ViewModels
{
    public class ExamViewModel
    {
        public ExamSchedule Exam { get; set; } = new();

        public List<SelectListItem> Courses { get; set; } = new();

        public List<SelectListItem> Branches { get; set; } = new();

        public List<SelectListItem> Subjects { get; set; } = new();

        public List<ExamSchedule> Exams { get; set; } = new();
    }
}