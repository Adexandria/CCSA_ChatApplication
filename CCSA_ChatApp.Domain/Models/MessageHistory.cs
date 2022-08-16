using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain.Models
{
    public class MessageHistory
    {
        public virtual Guid HistoryId { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
        public virtual GroupChat GroupChatUser { get; set; }
        public virtual Message Message { get; set; }
    }
}
