using CCSA_ChatApp.Db.Repositories;
using CCSA_ChatApp.Domain.DTOs.MessageDTOs;
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
        public MessageHistoryService(MessageHistoryRepository messageHistoryRepository)
        {
            _messageHistoryRepo = messageHistoryRepository;
        }
        public IEnumerable<MessageDTO> FetchGroupChatMessagesByGroupId(Guid groupId)
        {
           var histories = _messageHistoryRepo.GetMessageHistoryByGroupId(groupId);
            var messages = new List<MessageDTO>();
            foreach (var history in histories)
            {
                messages.Add(
                    new MessageDTO { 
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
                        MessageCreated = history.Message.MessageCreated,
                        TextMessage = history.Message.TextMessage
                    });
            }
            return messages;
        }
    }
}
