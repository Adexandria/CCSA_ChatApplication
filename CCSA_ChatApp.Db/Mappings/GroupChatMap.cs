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
            Map(s => s.Picture);
            HasManyToMany(s => s.Members).Cascade.All().Inverse().Table("GroupChatMember");
            HasMany(s => s.ChatHistory);

        }
    }
}
