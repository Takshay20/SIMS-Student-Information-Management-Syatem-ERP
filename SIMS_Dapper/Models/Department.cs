namespace SIMS_Dapper.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentCode { get; set; }

        public bool IsActive { get; set; }
    }
}
