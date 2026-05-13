namespace SIMS_Dapper.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public int UserId { get; set; }

        public int? ParentId { get; set; }

        public string StudentName { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public string EnrollmentNo { get; set; }

        public string RollNo { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public DateTime? DOB { get; set; }

        public int CourseId { get; set; }

        public int BranchId { get; set; }

        public int SectionId { get; set; }

        public int Semester { get; set; }

        public string Session { get; set; }

        public string Address { get; set; }

        public string PhotoPath { get; set; }

        public bool IsActive { get; set; }
    }
}