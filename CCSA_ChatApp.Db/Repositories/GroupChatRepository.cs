using CCSA_ChatApp.Domain.Models;
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

        public async Task CreateGroupChat(GroupChat group)
        {
             await _session.SaveAsync(group);
            Commit();
        }
       
        public async Task<GroupChat> GetGroupChatById(Guid groupChatId)
        {
            var groupChat = await _session.Query<GroupChat>().FirstOrDefaultAsync(x => x.GroupId == groupChatId);
            return groupChat;
        }

        public async Task DeleteGroupChat(Guid groupChatId)
        {
            var groupChat = GetGroupChatById(groupChatId);
            if (groupChat != null)
            {
                await _session.DeleteAsync(groupChatId);
                Commit();
            }
        }

        /* public void UpdateGroupChatByName(GroupChat name)
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
        }*/


    }
}
