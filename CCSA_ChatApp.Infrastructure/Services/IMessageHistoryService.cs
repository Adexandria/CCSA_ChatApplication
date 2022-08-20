using CCSA_ChatApp.Domain.DTOs.MessageDTOs;
using CCSA_ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public interface IMessageHistoryService
    {
        Task SendMessage(string text, Guid senderId, string receiverUsername);
        Task SendMessageToGroup(string text, Guid senderId, Guid groupChatId);
        IEnumerable<MessageDTO> FetchMessagesBySenderId(Guid senderId);
        IEnumerable<MessageDTO> FetchMessagesByReceiverUsername(string recieverUsername);
        IEnumerable<MessageDTO> FetchGroupChatMessagesByGroupId(Guid groupId);
    }
}
