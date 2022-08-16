﻿using CCSA_ChatApp.Domain.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db.Repositories
{
    public class GroupChatRepository : Repository<GroupChat>
    {
        public GroupChatRepository(SessionFactory sessionFactory) : base(sessionFactory)
        {

        }

        public void CreateGroupChat(GroupChat group)
        {
            _session.Save(group);
        }

        public void UpdateGroupChatByName(GroupChat name)
        {
            _session.Update(name);
            Commit();
        }

        public void UpdateGroupChatByPicture(GroupChat picture)
        {
            _session.Update(picture);
            Commit();
        }

        public void UpdateGroupChatByDescription(GroupChat description)
        {
            _session.Update(description);
            Commit();
        }

        public GroupChat? GetGroupChatById(Guid groupChatId)
        {
            var groupChat = _session.Query<GroupChat>().FirstOrDefault(x => x.GroupId == groupChatId);
            return groupChat;
        }

        public void DeleteGroupChat(Guid groupChatId)
        {
            var groupChat = GetGroupChatById(groupChatId);
            if (groupChat != null)
            {
                _session.Delete(groupChatId);
                Commit();
            }
        }


    }
}