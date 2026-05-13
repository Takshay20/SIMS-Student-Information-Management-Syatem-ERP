namespace SIMS_Dapper.Models
{
    public class AttendanceRecord
    {
        public int AttendanceId { get; set; }

        public int SessionId { get; set; }

        public int StudentId { get; set; }

        public string Status { get; set; }

        public DateTime MarkedAt { get; set; }

        public string StudentName { get; set; }

        public string EnrollmentNo { get; set; }
    }
}