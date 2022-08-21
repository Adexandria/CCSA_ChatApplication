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
        IEnumerable<RetrieveMessageDTO> FetchMessagesBySenderId(Guid senderId);
        IEnumerable<RetrieveMessageDTO> FetchMessagesByReceiverUsername(string recieverUsername);
        IEnumerable<RetrieveMessageDTO> FetchGroupChatMessagesByGroupName(string groupName);
    }
}
