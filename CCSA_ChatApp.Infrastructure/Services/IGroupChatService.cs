using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.Models;
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
        Task Delete(Guid groupId);
        IEnumerable<GroupChatDTO> GetAll();
        Task UpdateGroupName(Guid groupId,string name);
        Task UpdateGroupDescription(Guid groupId,string description);
        Task UpdateGroupPicture(Guid groupId, byte[] picture);
        Task CreateGroupChat(string name, string groupDescription, byte[] picture);
    }
}
