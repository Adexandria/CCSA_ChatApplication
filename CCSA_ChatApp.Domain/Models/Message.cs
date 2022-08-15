using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain.Models
{
    public class Message
    {
        public virtual Guid Id { get; set; }
        public virtual string TextMessage { get; set; }
        public virtual DateTime MessageCreated { get; set; }
    }
}
