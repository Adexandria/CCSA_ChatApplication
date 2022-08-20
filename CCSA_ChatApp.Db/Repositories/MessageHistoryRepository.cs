using CCSA_ChatApp.Domain.Models;
using NHibernate.Linq;

namespace CCSA_ChatApp.Db.Repositories
{
    public class MessageHistoryRepository : Repository<MessageHistory>
    {
        public MessageHistoryRepository(SessionFactory sessionFactory) : base(sessionFactory)
        {

        }

        public async Task CreateMessageHistory(MessageHistory messageHistory)
        {
            await _session.SaveAsync(messageHistory);
            await Commit();
        }

        public IEnumerable<MessageHistory> GetMessageHistoryBySenderId(Guid senderId)
        {
            var messageHistory =  _session.Query<MessageHistory>().Where(m => m.Sender.UserId == senderId);
            return messageHistory;
        }

        public IEnumerable<MessageHistory> GetMessageHistoryByGroupId(Guid groupId)
        {
            var messageHistory = _session.Query<MessageHistory>().Where(m => m.GroupChatUser.GroupId == groupId);
            return messageHistory;
        }

        public IEnumerable<MessageHistory> GetMessageHistoryByRetrieverUsername(string username)
        {
            var messageHistory1 = _session.Query<MessageHistory>().OrderBy(s=>s.Sender);
            var messageHistory = _session.Query<MessageHistory>().Where(m => m.Receiver.Profile.Username == username);
            return messageHistory;
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
