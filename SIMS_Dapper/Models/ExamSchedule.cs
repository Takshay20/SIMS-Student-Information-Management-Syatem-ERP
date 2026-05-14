namespace SIMS_Dapper.Models
{
    public class ExamSchedule
    {
        public int ExamScheduleId { get; set; }

        public int CourseId { get; set; }

        public int BranchId { get; set; }

        public int Semester { get; set; }

        public int SubjectId { get; set; }

        public string ExamType { get; set; }

        public DateTime ExamDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string Venue { get; set; }

        public string CourseName { get; set; }

        public string BranchName { get; set; }

        public string SubjectName { get; set; }
    }
}