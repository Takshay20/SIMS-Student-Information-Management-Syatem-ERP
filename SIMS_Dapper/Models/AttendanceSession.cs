namespace SIMS_Dapper.Models
{
    public class AttendanceSession
    {
        public int SessionId { get; set; }

        public int SubjectId { get; set; }

        public int SectionId { get; set; }

        public int FacultyId { get; set; }

        public DateTime SessionDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string TopicCovered { get; set; }

        public string Remarks { get; set; }

        public bool IsLocked { get; set; }
    }
}