using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.DTOs.MessageDTOs;
using CCSA_ChatApp.Domain.DTOs.UserDTOs;

namespace CCSA_ChatApp.Domain.DTOs.MessageHistoryDTOs
{
    public class MessageHistoryDTO
    {
        public virtual UserDTO Sender { get; set; }
        public virtual UserDTO? Receiver { get; set; }
        public virtual GroupChatDTO? GroupChatUser { get; set; }
        public virtual MessageDTO Message { get; set; }
    }
}