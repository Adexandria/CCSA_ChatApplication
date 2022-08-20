using CCSA_ChatApp.Domain.DTOs.MessageDTOs;
using CCSA_ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public interface IMessageService
    {
        Task SendMessage(string text, Guid senderId, string receiverUsername);
        Task SendMessageToGroup(string text, Guid senderId, Guid groupChatId);
        Task UpdateMessageById(string text, Guid messageId);
        Task DeleteMessageByMessageId(Guid messageId);
        

    }
}
