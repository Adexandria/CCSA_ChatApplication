namespace CCSA_ChatApp.Domain.Models
{
    public class UserRole
    {
        public Guid RoleId { get; set; }
        public string Role { get; set; }
        public User User { get; set; }
    }
}
