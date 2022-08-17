using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.DTOs.MessageHistoryDTOs;

namespace CCSA_ChatApp.Domain.DTOs.UserDTOs
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<UsersDTO> Contacts { get; set; }
        public List<MessageHistoryDTO> Histories { get; set; }
        public List<GroupChatDTO> GroupChats { get; set; } 
    }
}
