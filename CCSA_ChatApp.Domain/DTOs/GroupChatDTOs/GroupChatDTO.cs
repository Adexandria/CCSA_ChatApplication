using CCSA_ChatApp.Domain.DTOs.MessageHistoryDTOs;
using CCSA_ChatApp.Domain.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;

namespace CCSA_ChatApp.Domain.DTOs.GroupChatDTOs
{
    public class GroupChatDTO
    {
        public  string GroupName { get; set; }
        public  string GroupDescription { get; set; }
        public  byte[] Picture { get; set; }
        public DateTime CreatedDate { get; protected set; }
        public  UserDTO CreatedBy { get; set; }
        public  List<UsersDTO> Members { get; set; } 
        public  List<MessageHistoryDTO> ChatHistory { get; set; } 
    }
}