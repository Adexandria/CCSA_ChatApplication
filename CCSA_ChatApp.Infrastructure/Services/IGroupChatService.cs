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
        void CreateGroupChat(GroupChat group);
        void Delete(Guid groupId);
        IEnumerable<GroupChatDTO> GetAll();
        void UpdateGroupName(Guid groupId,string name);
        void UpdateGroupDescription(Guid groupId,string description);
        void UpdateGroupPicture(Guid groupId,string picture);
    }
}
