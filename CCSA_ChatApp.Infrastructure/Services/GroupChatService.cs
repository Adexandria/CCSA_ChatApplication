using CCSA_ChatApp.Db.Repositories;
using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public class GroupChatService : IGroupChatService
    {
        private readonly GroupChatRepository _groupChatRepository;
        public GroupChatService(GroupChatRepository groupChatRepository)
        {
            _groupChatRepository = groupChatRepository;
        }
        
        public async Task CreateGroupChat(GroupChat group)
        {
             await _groupChatRepository.Create(group);
        }


        public IEnumerable<GroupChatsDTO> GetAll(Guid userId)
        {
            var groupChats = _groupChatRepository.GetAllGroupChatsByUserId(userId);
            return groupChats.Adapt<IEnumerable<GroupChatsDTO>>();
        }

        public async Task<GroupChatDTO> GetGroupChat(Guid groupId)
        {
            GroupChat currentGroupChat = await _groupChatRepository.GetGroupChatById(groupId);
            return currentGroupChat.Adapt<GroupChatDTO>();
        }
        public async Task UpdateGroupDescription(Guid groupId,string description)
        {
            GroupChat currentGroupChat = await _groupChatRepository.GetGroupChatById(groupId);
            if (currentGroupChat is not null)
            {
                currentGroupChat.GroupDescription = description;
                await _groupChatRepository.Update(currentGroupChat);
            }
        }
        
        public async Task UpdateGroupName(Guid groupId,string name)
        {
            GroupChat currentGroupChat = await _groupChatRepository.GetGroupChatById(groupId);
            if (currentGroupChat is not null)
            {
                currentGroupChat.GroupName = name;
                await _groupChatRepository.Update(currentGroupChat);
            }
        }
        public async Task UpdateGroupPicture(IFormFile picture, GroupChat group)
        {
            var image = ConvertFromImageToByte(picture);
            group.Picture = image;
           await  _groupChatRepository.Update(group);
        }

        
        public async Task DeleteGroupPicture(GroupChat group)
        {
            group.Picture = null;
            await _groupChatRepository.Update(group);
        }

        public async Task AddUserToGroup(Guid groupId, User currentUser)
        {
            await _groupChatRepository.AddUserToGroup(groupId, currentUser);
        }

        public async Task RemoveUserToGroup(Guid groupId, User currentUser)
        {
            await _groupChatRepository.RemoveUserToGroup(groupId, currentUser);
        }
        
        public byte[] ConvertFromImageToByte(IFormFile picture)
        {
            if (picture.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    picture.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return fileBytes;
                }
            }
            return default;
        }

        public async Task DeleteGroupChatById(Guid groupId)
        {
           await _groupChatRepository.DeleteGroupChat(groupId);
        }

      
    }
}
