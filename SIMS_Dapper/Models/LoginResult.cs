namespace SIMS_Dapper.Models
{
    public class LoginResult
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsActive { get; set; }
    }
}
