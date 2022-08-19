using CCSA_ChatApp.Domain.DTOs.UserDTOs;

namespace CCSA_ChatApp.Domain.DTOs.GroupChatDTOs
{
    public class GroupChatsDTO
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public UsersDTO CreatedBy { get; set; }
        public List<UsersDTO> Members { get; set; }
    }
}
