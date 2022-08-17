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
             await _groupChatRepository.CreateGroupChat(group);
        }

        public async Task CreateGroupChat (string name, string groupDescription, byte[] picture)
        {
            await _groupChatRepository.CreateGroupChat(new GroupChat
            {
                GroupName = name,
                GroupDescription = groupDescription,
                Picture = picture
            });
        }

        

        public async Task CreateGroupChat(NewGroupChatDTO newGroupChat)
        {
            await _groupChatRepository.CreateGroupChat(newGroupChat.GroupName, newGroupChat.GroupDescription, newGroupChat.Picture);
        }

        public async Task Delete(Guid groupId)
        {
            GroupChat currentGroupChat = await _groupChatRepository.GetGroupChatById(groupId);
            if(currentGroupChat is not null)
            {
                _groupChatRepository.Delete(currentGroupChat);
            }
        }

        public IEnumerable<GroupChatDTO> GetAll()
        {
            var groupChats = _groupChatRepository.GetAll();
            return groupChats.Adapt<IEnumerable<GroupChatDTO>>();
        }

        public async Task UpdateGroupDescription(Guid groupId,string description)
        {
            GroupChat currentGroupChat = await _groupChatRepository.GetGroupChatById(groupId);
            if (currentGroupChat is not null)
            {
                currentGroupChat.GroupDescription = description;
                _groupChatRepository.Update(currentGroupChat);
            }
        }
        
        public async Task UpdateGroupName(Guid groupId,string name)
        {
            GroupChat currentGroupChat = await _groupChatRepository.GetGroupChatById(groupId);
            if (currentGroupChat is not null)
            {
                currentGroupChat.GroupName = name;
                _groupChatRepository.Update(currentGroupChat);
            }
        }
        public void UpdateGroupPicture(IFormFile picture, GroupChat group)
        {
            var image = ConvertFromImageToByte(picture);
            group.Picture = image;
            _groupChatRepository.Update(group);
        }

        //public async Task UpdateGroupPicture(Guid groupId, string picture)
        public void DeleteGroupPicture(GroupChat group)
        {
            group.Picture = null;
            _groupChatRepository.Update(group);
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

        public Task UpdateGroupPicture(Guid groupId, string picture)
        {
            throw new NotImplementedException();
        }
    }
}
