namespace SIMS_Dapper.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }

        public int UserId { get; set; }

        public string FacultyName { get; set; }

        public string EmployeeCode { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string Designation { get; set; }

        public bool IsActive { get; set; }
    }
}