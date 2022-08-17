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
        void SendMessage(string text, string senderUsername, string receiverUsername);
        void SendMessageToGroup(string text, string senderUsername, Guid groupChatId);
        void UpdateMessage(string text);
        void DeleteMessageByMessageId(Guid messageId);
        

    }
}
