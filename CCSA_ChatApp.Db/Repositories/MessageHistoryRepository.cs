using CCSA_ChatApp.Domain.Models;
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
        public void CreateMessageHistory(Message message, MessageHistory messageHistory)
        {
            messageHistory.Message = message;
            _session.Save(message);
            Commit();
        }

        public MessageHistory? GetMessageHistoryBySenderId(Guid senderId)
        {
            var messageHistory = _session.Query<MessageHistory>().FirstOrDefault(m => m.Sender.UserId == senderId);
            return (messageHistory);
        }

        public MessageHistory? GetMessageHistoryByRetrieverUsername(string username)
        {
            var messageHistory = _session.Query<MessageHistory>().FirstOrDefault(m => m.Receiver.Profile.Username == username);
            return (messageHistory);
        }

        public void DeleteMessageHIstoryById(Guid messageId)
        {
            var messageHistory = _session.Query<MessageHistory>().FirstOrDefault(m => m.Message.MessageId == messageId);
            if (messageHistory != null)
            {
                _session.Delete(messageHistory);
                Commit();
            }

        }
    }
}
