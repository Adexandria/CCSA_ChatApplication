using CCSA_ChatApp.Domain.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db.Repositories
{
    public class MessageHistoryRepository : Repository<MessageHistory>
    {
        public MessageHistoryRepository(SessionFactory sessionFactory) : base(sessionFactory)
        {

        }

        private async new Task Commit()
        {
            using var transction = _session.BeginTransaction();
            try
            {
                if (transction.IsActive)
                {
                    await transction.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                transction.Rollback();
            }
        }

        public async Task CreateMessageHistory(Message message, MessageHistory messageHistory)
        {
            messageHistory.Message = message;
            await _session.SaveAsync(messageHistory);
            await Commit();
        }

        public IEnumerable<MessageHistory> GetMessageHistoryBySenderId(Guid senderId)
        {
            var messageHistory =  _session.Query<MessageHistory>().Where(m => m.Sender.UserId == senderId);
            return (messageHistory);
        }

        public IEnumerable<MessageHistory> GetMessageHistoryByGroupId(Guid groupId)
        {
            var messageHistory = _session.Query<MessageHistory>().Where(m => m.GroupChatUser.GroupId == groupId);
            return (messageHistory);
        }

        public IEnumerable<MessageHistory> GetMessageHistoryByRetrieverUsername(string username)
        {
            var messageHistory = _session.Query<MessageHistory>().Where(m => m.Sender.Profile.Username == username);
            return (messageHistory);
        }

        public async Task DeleteMessageHIstoryById(Guid messageId)
        {
            var messageHistory = await _session.Query<MessageHistory>().FirstOrDefaultAsync(m => m.Message.MessageId == messageId);
            if (messageHistory != null)
            {
                await _session.DeleteAsync(messageHistory);
                await Commit();
            }

        }
    }
}
