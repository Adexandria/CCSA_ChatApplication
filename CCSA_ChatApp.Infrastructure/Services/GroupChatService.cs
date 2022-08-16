﻿using CCSA_ChatApp.Db.Repositories;
using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public class GroupChatService : IGroupChatService
    {
        private readonly GroupChatRepository _groupChatRepository;
        public GroupChatService(GroupChatRepository groupChatRepository)
        {
            _groupChatRepository = groupChatRepository;
        }
        public void CreateGroupChat(GroupChat group)
        {
            _groupChatRepository.CreateGroupChat(group);
        }

        public void Delete(Guid groupId)
        {
            GroupChat currentGroupChat = _groupChatRepository.GetGroupChatById(groupId);
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

        public void UpdateGroupDescription(Guid groupId,string description)
        {
            GroupChat currentGroupChat = _groupChatRepository.GetGroupChatById(groupId);
            if (currentGroupChat is not null)
            {
                currentGroupChat.GroupDescription = description;
                _groupChatRepository.Update(currentGroupChat);
            }
        }
        
        public void UpdateGroupName(Guid groupId,string name)
        {
            GroupChat currentGroupChat = _groupChatRepository.GetGroupChatById(groupId);
            if (currentGroupChat is not null)
            {
                currentGroupChat.GroupName = name;
                _groupChatRepository.Update(currentGroupChat);
            }
        }

        public void UpdateGroupPicture(Guid groupId,string picture)
        {
            GroupChat currentGroupChat = _groupChatRepository.GetGroupChatById(groupId);
            if (currentGroupChat is not null)
            {
                currentGroupChat.Picture = picture;
                _groupChatRepository.Update(currentGroupChat);
            }
        }
    }
}