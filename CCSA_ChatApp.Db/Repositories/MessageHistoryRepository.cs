using CCSA_ChatApp.Domain.Models;
using NHibernate.Linq;

namespace CCSA_ChatApp.Db.Repositories
{
    public class MessageHistoryRepository : Repository<MessageHistory>
    {
        public MessageHistoryRepository(SessionFactory sessionFactory) : base(sessionFactory)
        {

        }

        public async Task CreateMessageHistory(Message message, MessageHistory messageHistory)
        {
            messageHistory.Message = message;
            messageHistory.HistoryId = Guid.NewGuid();
            await _session.SaveAsync(messageHistory);
            await Commit();
        }

        public IEnumerable<MessageHistory> GetMessageHistoryBySenderId(Guid senderId)
        {
            var messageHistory =  _session.Query<MessageHistory>().Where(m => m.Sender.UserId == senderId);
            return messageHistory;
        }

        public IEnumerable<MessageHistory> GetMessageHistoryByGroupName(string groupName)
        {
            var messageHistory = _session.Query<MessageHistory>().Where(m => m.GroupChatUser.GroupName == groupName).OrderBy(s=>s.Message.MessageCreated);
            return messageHistory;
        }

        public IEnumerable<MessageHistory> GetMessageHistoryByRetrieverUsername(string sender,string username)
        {
            var messageHistory1 = _session.Query<MessageHistory>().OrderBy(s=>s.Message.MessageCreated);
            var messageHistory = _session.Query<MessageHistory>().Where(m => m.Receiver.Profile.Username == username && m.Sender.Profile.Username== sender);
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
