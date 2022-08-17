using CCSA_ChatApp.Domain.DTOs.MessageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public interface IMessageHistoryService
    {
        List<MessageDTO> FetchMessagesBySenderId(Guid senderId);
        List<MessageDTO> FetchMessagesByRecieverUsername(string recieverUsername);
        List<MessageDTO> FetchGroupChatMessagesByGroupId(Guid groupId);
    }
}
