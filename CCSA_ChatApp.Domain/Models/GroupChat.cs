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
        public virtual byte[] Picture { get; set; }
        public virtual DateTime CreatedDate { get; protected set; }
        public virtual User CreatedBy { get; set; }
        public virtual IList<User> Members { get; set; } = new List<User>();
        public virtual IList<MessageHistory> ChatHistory { get; set; } = new List<MessageHistory>();
    }
}
