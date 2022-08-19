using CCSA_ChatApp.Domain.Models;
using FluentNHibernate.Mapping;

namespace CCSA_ChatApp.Db.Mappings
{
    public class MessageHistoryMap : ClassMap<MessageHistory>
    {
        public MessageHistoryMap()
        {
            Table("MessageHistories");
            Id(hist => hist.HistoryId).GeneratedBy.Guid();
            References(hist => hist.Sender).Cascade.All();
            References(hist => hist.Receiver).Cascade.All();
            References(hist => hist.Message);
            References(hist => hist.GroupChatUser).Cascade.All();
        }
    }
}
