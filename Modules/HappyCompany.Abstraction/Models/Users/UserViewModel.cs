namespace HappyCompany.Abstraction.Models.Users
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}