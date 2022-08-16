namespace CCSA_ChatApp.Domain.Models
{
    public class UserRole
    {
        public virtual Guid RoleId { get; set; }
        public virtual string Role { get; set; }
        public virtual User User { get; set; }
    }
}
