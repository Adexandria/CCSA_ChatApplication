using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.Models;
using Microsoft.AspNetCore.Http;


namespace CCSA_ChatApp.Infrastructure.Services
{
    public interface IGroupChatService
    {
        Task CreateGroupChat(GroupChat group);
        Task DeleteGroupChatById(Guid groupId);
        IEnumerable<GroupChatsDTO> GetAll(Guid userId);
        Task<GroupChatDTO> GetGroupChat(Guid groupId);
        Task<GroupChat> GetGroupChatByName(string name);
        Task UpdateGroupName(Guid groupId, string name);
        Task UpdateGroupDescription(Guid groupId, string description);
        Task UpdateGroupPicture(IFormFile picture, GroupChat group);
        Task AddUserToGroup(Guid groupId, User currentUser);
        Task RemoveUserToGroup(Guid groupId, User currentUser);
        Task DeleteGroupPicture(GroupChat group);
        byte[] ConvertFromImageToByte(IFormFile picture);
    }
}
