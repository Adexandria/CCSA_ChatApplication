using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public interface IGroupChatService
    {
        Task CreateGroupChat(GroupChat group);
        Task DeleteGroupChatById(Guid groupId);
        IEnumerable<GroupChatsDTO> GetAll();
        Task<GroupChatDTO> GetGroupChat(Guid groupId);
        Task UpdateGroupDescription(Guid groupId, string description);
        Task UpdateGroupPicture(IFormFile picture, GroupChat group);
        Task DeleteGroupPicture(GroupChat group);
        byte[] ConvertFromImageToByte(IFormFile picture);
    }
}
