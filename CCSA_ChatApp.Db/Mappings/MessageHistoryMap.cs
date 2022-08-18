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
            References(hist => hist.Sender).Cascade.Delete().Not.Nullable();
            References(hist => hist.Receiver).Cascade.Delete();
            References(hist => hist.Message).Cascade.Delete().Unique();
            References(hist => hist.GroupChatUser).Cascade.Delete();
        }
    }
}
