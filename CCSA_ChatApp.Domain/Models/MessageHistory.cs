using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain.Models
{
    public class MessageHistory
    {
        public Guid Id { get; set; }
        public User SenderUser { get; set; }
        public User ReceiverUser { get; set; }
        public GroupChat GroupChatUser { get; set; }
        public Message Message { get; set; }
    }
}
