using CCSA_ChatApp.Db.Repositories;
using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.Models;
using Mapster;

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

        public async Task CreateGroupChat (string name, string groupDescription, string picture)
        {
            await _groupChatRepository.CreateGroupChat(new GroupChat
            {
                GroupName = name,
                GroupDescription = groupDescription,
                Picture = picture
            });
        }

        public Task CreateGroupChat(string name, string groupDescription, byte[] picture)
        {
            throw new NotImplementedException();
        }

        public async Task CreateGroupChat(NewGroupChatDTO newGroupChat)
        {
            await _groupChatRepository.CreateGroupChat(newGroupChat.GroupName, newGroupChat.GroupDescription, newGroupChat.Picture);
            //throw new NotImplementedException();
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

        public async Task UpdateGroupPicture(Guid groupId, string picture)
        {
            GroupChat currentGroupChat = await _groupChatRepository.GetGroupChatById(groupId);
            if (currentGroupChat is not null)
            {
                currentGroupChat.Picture = picture;
                _groupChatRepository.Update(currentGroupChat);
            }
        }
    }
}
