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
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(SessionFactory sessionFactory, MessageHistoryRepository messageHistoryRepo, UserRepository userRepo, GroupChatRepository groupChatRepo) : base(sessionFactory)
        {
            _messageHistoryRepository = messageHistoryRepo;
            _userRepository = userRepo;
            _groupChatRepository = groupChatRepo;
        }

        private readonly MessageHistoryRepository _messageHistoryRepository;
        private readonly UserRepository _userRepository;
        private readonly GroupChatRepository _groupChatRepository;


        private async new Task  Commit()
        {
            using var transction =  _session.BeginTransaction();
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

        public async Task CreateMessage(Message message, Guid senderId, string receiverUsername)
        {
            var sender = await _userRepository.GetUserById(senderId);
            var reciever = await _userRepository.GetUserByUsername(receiverUsername);
            if (sender != null)
            {
                if (reciever != null)
                {
                    var messageHistory = new MessageHistory { Receiver = reciever, Sender = sender };
                    await _session.SaveAsync(message);
                    await _messageHistoryRepository.CreateMessageHistory(message, messageHistory);
                }
                throw new Exception("This user doesn't exist");
            }
            throw new Exception("You're not a registered user");
        }

        public async Task CreateMessageForGroup(Message message, Guid senderId, Guid groupId)
        {
            var sender = await _userRepository.GetUserById(senderId);
            var groupChat = await _groupChatRepository.GetGroupChatById(groupId);
            
            if (sender != null)
            {
                if (groupChat != null)
                {
                    var messageHistory = new MessageHistory { GroupChatUser = groupChat, Sender = sender };
                    await _session.SaveAsync(message);
                    await _messageHistoryRepository.CreateMessageHistory(message, messageHistory);
                }
                throw new Exception("This group doesn't exist yet");
            }
            throw new Exception("You're not a registered user");
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
            var message = GetMessageById(messageId);
            if (message != null)
            {
                await _session.DeleteAsync(message);
                await Commit();
            }

        }

        public async Task UpdateMessage(Message message)
        {
            await _session.UpdateAsync(message);
            await Commit();
        }
    }
}
