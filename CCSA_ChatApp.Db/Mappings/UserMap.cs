using CCSA_ChatApp.Domain.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(user => user.UserId).GeneratedBy.Guid();
            Map(user => user.FirstName);
            Map(user => user.MiddleName);
            Map(user => user.LastName);
            Map(user => user.Email);
            HasMany(user => user.Histories);
            HasManyToMany(user => user.GroupChats).Cascade.All().Table("GroupChatMember");

        }
    }
}
