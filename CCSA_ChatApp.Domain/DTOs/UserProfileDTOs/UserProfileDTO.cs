using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.DTOs.UserDTOs;

namespace CCSA_ChatApp.Domain.DTOs.UserProfileDTOs
{
    public class UserProfileDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<UsersDTO> Contacts { get; set; }
        public virtual string Country { get; set; }
        public List<GroupChatDTO> GroupChats { get; set; }
        public virtual string Username { get; set; }
        public virtual string Picture { get; set; }
        
    }
}
