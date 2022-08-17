using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain.Models
{
    public class RefreshToken
    {
        public virtual Guid TokenId { get; set; }
        public virtual string Token { get; set; }
        public virtual DateTime ExpiryDate { get; set; }
        public virtual User User { get; set; }
    }
}
