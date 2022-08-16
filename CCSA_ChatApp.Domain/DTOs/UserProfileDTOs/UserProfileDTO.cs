using CCSA_ChatApp.Domain.DTOs.UserDTOs;

namespace CCSA_ChatApp.Domain.DTOs.UserProfileDTOs
{
    public class UserProfileDTO
    {
        public virtual string Country { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual string Username { get; set; }
        public virtual string Picture { get; set; }
        
    }
}
