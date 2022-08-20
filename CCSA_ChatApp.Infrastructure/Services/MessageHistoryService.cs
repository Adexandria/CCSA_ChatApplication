using CCSA_ChatApp.Db.Repositories;
using CCSA_ChatApp.Domain.DTOs.MessageDTOs;
using CCSA_ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public class MessageHistoryService : IMessageHistoryService
    {
        private readonly MessageHistoryRepository _messageHistoryRepo;
        private readonly MessageRepository _messageRepository;

        public MessageHistoryService(MessageHistoryRepository messageHistoryRepository, MessageRepository messageRepository)
        {
            _messageHistoryRepo = messageHistoryRepository;
            _messageRepository = messageRepository;
        }

        public async Task SendMessage(string text, Guid senderId, string receiverUsername)
        {
            var message = new Message { TextMessage = text, MessageCreated = DateTime.Now };
            await _messageRepository.CreateMessage(message, senderId, receiverUsername);

        }

        public async Task SendMessageToGroup(string text, Guid senderId, Guid groupId)
        {
            var message = new Message { TextMessage = text, MessageCreated = DateTime.Now };
            await _messageRepository.CreateMessageForGroup(message, senderId, groupId);
        }

        public IEnumerable<MessageDTO> FetchGroupChatMessagesByGroupId(Guid groupId)
        {
           var histories = _messageHistoryRepo.GetMessageHistoryByGroupId(groupId);
            var messages = new List<MessageDTO>();
            foreach (var history in histories)
            {
                messages.Add(
                    new MessageDTO { 
                        MessageId = history.Message.MessageId,
                        MessageCreated = history.Message.MessageCreated, 
                        TextMessage = history.Message.TextMessage 
                    });
            }
            return messages;
        }

        public IEnumerable<MessageDTO> FetchMessagesByReceiverUsername(string receiverUsername)
        {
            var histories = _messageHistoryRepo.GetMessageHistoryByRetrieverUsername(receiverUsername);
            var messages = new List<MessageDTO>();
            foreach (var history in histories)
            {
                messages.Add(
                    new MessageDTO
                    {
                        MessageId = history.Message.MessageId,
                        MessageCreated = history.Message.MessageCreated,
                        TextMessage = history.Message.TextMessage
                    });
            }
            return messages;
        }

        public IEnumerable<MessageDTO> FetchMessagesBySenderId(Guid senderId)
        {
            var histories = _messageHistoryRepo.GetMessageHistoryBySenderId(senderId);
            var messages = new List<MessageDTO>();
            foreach (var history in histories)
            {
                messages.Add(
                    new MessageDTO
                    {
                        MessageId = history.Message.MessageId,
                        MessageCreated = history.Message.MessageCreated,
                        TextMessage = history.Message.TextMessage
                    });
            }
            return messages;
        }
    }
}
