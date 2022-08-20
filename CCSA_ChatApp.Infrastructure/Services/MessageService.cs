using CCSA_ChatApp.Db.Repositories;
using CCSA_ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly MessageRepository _messageRepository;
 
        public MessageService(MessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
          
        }

        public async Task DeleteMessageByMessageId(Guid messageId)
        {
           await _messageRepository.DeleteMessageById(messageId);
        }

        public async Task<Message> SendMessage(string text)
        {
             var message = new Message { TextMessage = text, MessageCreated = DateTime.Now };
             await _messageRepository.CreateMessage(message);
            return message;
            
        }

        public async Task<Message> SendMessageToGroup(string text)
        {
            var message = new Message { TextMessage = text, MessageCreated = DateTime.Now };
            await _messageRepository.CreateMessage(message);
            return message;
            
        }

        public async Task UpdateMessageById(string text, Guid messageId)
        {
            var message = await _messageRepository.GetMessageById(messageId);
            if (message != null)
            {
                message.TextMessage = text;
                await _messageRepository.Update(message);
            }
            else
                throw new Exception("This Message does not exist anymore");
        }
    }
}
