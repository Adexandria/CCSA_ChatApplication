using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain.Models
{
    public class User
    {
        public virtual Guid UserId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual UserProfile Profile { get; set; }
        //public virtual IList<User> Contacts { get; set; } = new List<User>();
        public virtual IList<MessageHistory> Histories { get; set; } = new List<MessageHistory>();
        public virtual IList<GroupChat> GroupChats { get; set; } = new List<GroupChat>();
    }
}
