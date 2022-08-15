using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain.Models
{
    public class UserProfile
    {
        public virtual Guid ProfileId { get; set; }
        public virtual Country Country { get; set; }
        public virtual string Username { get; set; }
        public virtual string Picture { get; set; }
        public virtual User User { get; set; }
    }
}
