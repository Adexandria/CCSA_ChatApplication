using CCSA_ChatApp.Domain.Models;
using FluentNHibernate.Mapping;

namespace CCSA_ChatApp.Db.Mappings
{
    public class GroupChatMap : ClassMap<GroupChat>
    {
        public GroupChatMap()
        {
            Id(s => s.GroupId).GeneratedBy.Guid();
            Map(s => s.GroupName);
            Map(s => s.GroupDescription);
            Map(s => s.CreatedDate);
            References(s=>s.CreatedBy);
            Map(s => s.Picture).Length(int.MaxValue);
            HasManyToMany(s => s.Members);
            HasMany(s => s.ChatHistory);

        }
    }
}
