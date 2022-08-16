using CCSA_ChatApp.Domain.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db.Mappings
{
    public class MessageMap : ClassMap<Message>
    {
        public MessageMap()
        {
            Table("Messages");
            Id(message => message.MessageId).GeneratedBy.Guid();
            Map(message => message.TextMessage);
            Map(message => message.MessageCreated);

        }
    }
}
