using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain.Models
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual List<User> Users{ get; set; }
        public virtual List<MessageHistory> Histories { get; set; }
        public virtual List<GroupChat> GroupChats { get; set; }
    }
}
