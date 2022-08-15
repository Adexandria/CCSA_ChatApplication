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
        public User SenderUserId { get; set; }
        public User ReceiverUserId { get; set; }
        public GroupChat GroupChatUserId { get; set; }
        public Message MessageId { get; set; }
    }
}
