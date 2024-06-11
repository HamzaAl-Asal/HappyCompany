namespace HappyCompany.Context.DataAccess.Entities.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
       
        public bool IsActive { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}