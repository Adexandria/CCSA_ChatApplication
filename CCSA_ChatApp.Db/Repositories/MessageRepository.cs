using CCSA_ChatApp.Domain.Models;
using NHibernate.Linq;

namespace CCSA_ChatApp.Db.Repositories
{
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(SessionFactory sessionFactory) : base(sessionFactory)
        {
            
        }


        public async Task CreateMessage(Message message)
        {
            await _session.SaveAsync(message);
            await Commit();       
        }


        public async Task<Message> GetMessageById(Guid messageId)
        {
            var message = await _session.Query<Message>().FirstOrDefaultAsync(m => m.MessageId == messageId);
            if (message != null)
            {
                return (message);
            }
            throw new Exception("Message does not exist");
        }

       

        public async Task DeleteMessageById(Guid messageId)
        {
            var message = await GetMessageById(messageId);
            if (message != null)
            {
                await _session.DeleteAsync(message);
                await Commit();
            }

        }
    }
}
