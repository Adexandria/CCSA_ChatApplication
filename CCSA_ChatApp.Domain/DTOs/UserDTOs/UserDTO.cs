using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.DTOs.MessageHistoryDTOs;
using CCSA_ChatApp.Domain.DTOs.UserProfileDTOs;

namespace CCSA_ChatApp.Domain.DTOs.UserDTOs
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserProfileDTO UserProfile { get; set; }
        public List<MessageHistoryDTO> Histories { get; set; }
        public List<GroupChatDTO> GroupChats { get; set; } 
    }
}
