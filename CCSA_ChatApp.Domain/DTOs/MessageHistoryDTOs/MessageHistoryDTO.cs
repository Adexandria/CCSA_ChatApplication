using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.DTOs.MessageDTOs;
using CCSA_ChatApp.Domain.DTOs.UserDTOs;

namespace CCSA_ChatApp.Domain.DTOs.MessageHistoryDTOs
{
    public class MessageHistoryDTO
    {
        public UserDTO Sender { get; set; }
        public UserDTO? Receiver { get; set; }
        public GroupChatDTO? GroupChatUser { get; set; }
        public MessageDTO Message { get; set; }
    }
}