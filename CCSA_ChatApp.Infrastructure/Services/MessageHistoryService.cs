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
        public IEnumerable<RetrieveMessageDTO> FetchGroupChatMessagesByGroupName(string groupName)
        {
           var histories = _messageHistoryRepo.GetMessageHistoryByGroupName(groupName);
            var messages = new List<RetrieveMessageDTO>();
            foreach (var history in histories)
            {
                messages.Add(
                    new RetrieveMessageDTO { 
                        MessageId = history.Message.MessageId,
                        MessageCreated = history.Message.MessageCreated, 
                        TextMessage = history.Message.TextMessage,
                        GroupName = groupName
                    });
            }
            return messages;
        }

        public IEnumerable<RetrieveMessageDTO> FetchMessagesByReceiverUsername(string sender,string receiverUsername)
        {
            var histories = _messageHistoryRepo.GetMessageHistoryByRetrieverUsername(sender,receiverUsername);
            var messages = new List<RetrieveMessageDTO>();
            foreach (var history in histories)
            {
                messages.Add(
                    new RetrieveMessageDTO
                    {
                        MessageId = history.Message.MessageId,
                        MessageCreated = history.Message.MessageCreated,
                        TextMessage = history.Message.TextMessage,
                        Sender = history.Sender.Profile.Username,
                        RecieverUsername = receiverUsername
                    });
            }
            return messages;
        }
        
        public IEnumerable<RetrieveMessageDTO> FetchMessagesBySenderId(Guid senderId)
        {
            var histories = _messageHistoryRepo.GetMessageHistoryBySenderId(senderId);
            var messages = new List<RetrieveMessageDTO>();
            foreach (var history in histories)
            {
                var message = new RetrieveMessageDTO
                {
                    MessageId = history.Message.MessageId,
                    MessageCreated = history.Message.MessageCreated,
                    TextMessage = history.Message.TextMessage,
                    Sender= history.Sender.Profile.Username
                };
                if(history.Receiver != null)
                    message.RecieverUsername = history.Receiver.Profile.Username;
                if(history.GroupChatUser != null)
                    message.GroupName = history.GroupChatUser.GroupName;
                
                    messages.Add(message);
               
            }
            return messages;
        }
    }
}
