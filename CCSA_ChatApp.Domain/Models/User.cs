using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<User> Users{ get; set; }
        public List<MessageHistory> Histories { get; set; }
        public List<GroupChat> GroupChats { get; set; }
    }
}
