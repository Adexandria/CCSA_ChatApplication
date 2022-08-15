using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain.Models
{
    public class GroupChat
    {
        public virtual Guid GroupId { get; set; }
        public virtual string GroupName { get; set; }
        public virtual string GroupDescription { get; set; }
        public virtual string Picture { get; set; }
        public virtual List<User> Members { get; set; }
        public virtual List<MessageHistory> ChatHistory { get; set; }
    }
}
