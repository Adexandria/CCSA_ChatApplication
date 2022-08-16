using CCSA_ChatApp.Domain.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db.Repositories
{
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(SessionFactory sessionFactory, MessageHistoryRepository messageHistoryRepository) : base(sessionFactory)
        {
            _messageHistoryRepository = messageHistoryRepository;
        }

        private readonly MessageHistoryRepository _messageHistoryRepository;
        
        public void CreateMessage(Message message, MessageHistory messageHistory)
        {
            _session.Save(message);
            _messageHistoryRepository.CreateMessageHistory(message, messageHistory);
        }

        
        public Message? GetMessageById(Guid messageId)
        {
            var message = _session.Query<Message>().FirstOrDefault(m => m.MessageId == messageId);
            return (message);
        }

       

        public void DeleteMessageById(Guid messageId)
        {
            var message = GetMessageById(messageId);
            if (message != null)
            {
                _session.Delete(message);
                Commit();
            }

        }

        public void UpdateMessage(Message message)
        {
            _session.Update(message);
            Commit();
        }
    }
}
