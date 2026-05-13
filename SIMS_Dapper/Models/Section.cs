namespace SIMS_Dapper.Models
{
    public class Section
    {
        public int SectionId { get; set; }

        public int CourseId { get; set; }

        public int BranchId { get; set; }

        public int Semester { get; set; }

        public string SectionName { get; set; }

        public int Capacity { get; set; }

        public string CourseName { get; set; }

        public string BranchName { get; set; }
    }
}