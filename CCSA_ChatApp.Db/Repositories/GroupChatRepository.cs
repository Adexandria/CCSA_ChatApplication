using CCSA_ChatApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using NHibernate;
using NHibernate.Linq;
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
        
        public async Task<GroupChat> GetGroupChatById(Guid groupChatId)
        {
            var groupChat = await _session.Query<GroupChat>().FirstOrDefaultAsync(x => x.GroupId == groupChatId);
            return groupChat;
        }
        public async Task<GroupChat> GetGroupChatByName(string name)
        {
            var groupChat = await _session.Query<GroupChat>().Where(x => x.GroupName == name).FirstOrDefaultAsync();
            return groupChat;
        }

        public async Task DeleteGroupChat(Guid groupChatId)
        {
            var groupChat = await GetGroupChatById(groupChatId);
            if (groupChat != null)
            {
                await _session.DeleteAsync(groupChat);
                Commit();
            }
        }

        public async Task AddUserToGroup(Guid groupChatId, User currentUser)
        {
            var groupChat = await GetGroupChatById(groupChatId);
            if (groupChat != null)
            {
                groupChat.Members.Add(currentUser);
                await _session.UpdateAsync(groupChat);
                Commit();
            }
        }

        public async Task RemoveUserToGroup(Guid groupChatId, User currentUser)
        {
            var groupChat = await GetGroupChatById(groupChatId);
            if (groupChat != null)
            {
                var x = groupChat.Members.Where(s=>s.UserId == currentUser.UserId).FirstOrDefault();
                currentUser = x;
                groupChat.Members.Remove(currentUser);
                await _session.UpdateAsync(groupChat);
                Commit();
            }
        }

        public IEnumerable<GroupChat> GetAllGroupChatsByUserId(Guid userId)
        {
            var groupChat = _session.Query<GroupChat>().Where(x => x.CreatedBy.UserId == userId);
            return groupChat;

        }
    }
}
