using CCSA_ChatApp.Domain.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db.Mappings
{
    public class MessageHistoryMap : ClassMap<MessageHistory>
    {
        public MessageHistoryMap()
        {
            Table("MessageHistories");
            Id(hist => hist.HistoryId).GeneratedBy.Guid();
            References(hist => hist.Sender).Not.Nullable();
            References(hist => hist.Receiver);
            References(hist => hist.Message).Unique();
            References(hist => hist.GroupChatUser);
        }
    }
}
