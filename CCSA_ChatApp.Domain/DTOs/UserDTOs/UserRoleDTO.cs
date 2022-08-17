
namespace CCSA_ChatApp.Domain.DTOs.UserDTOs
{
    public class UserRoleDTO
    {
        public  Guid RoleId { get; set; }
        public string Role { get; set; }
        public  UserDTO User { get; set; }
    }
}
