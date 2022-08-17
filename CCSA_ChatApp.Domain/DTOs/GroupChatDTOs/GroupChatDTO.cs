using CCSA_ChatApp.Domain.DTOs.MessageHistoryDTOs;
using CCSA_ChatApp.Domain.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;

namespace CCSA_ChatApp.Domain.DTOs.GroupChatDTOs
{
    public class GroupChatDTO
    {
        public virtual Guid GroupId { get; set; }
        public virtual string GroupName { get; set; }
        public virtual string GroupDescription { get; set; }
        public virtual IFormFile Picture { get; set; }
        public virtual DateTime CreatedDate { get; protected set; }
        public virtual UserDTO CreatedBy { get; set; }
        public virtual List<UsersDTO> Members { get; set; } 
        public virtual List<MessageHistoryDTO> ChatHistory { get; set; } 
    }
}