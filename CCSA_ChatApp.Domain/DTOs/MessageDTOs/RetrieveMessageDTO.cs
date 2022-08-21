using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain.DTOs.MessageDTOs
{
    public class RetrieveMessageDTO
    {
        public Guid MessageId { get; set; }
        public string TextMessage { get; set; }
        public DateTime MessageCreated { get; set; }
        public string RecieverUsername { get; set; }
        public string GroupName { get; set; }

    }
}
