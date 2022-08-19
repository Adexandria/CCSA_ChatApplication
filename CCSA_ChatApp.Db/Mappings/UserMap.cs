using CCSA_ChatApp.Domain.Models;
using FluentNHibernate.Mapping;

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
            Map(x => x.Password);
            HasOne(s=>s.Profile).PropertyRef(s=>s.User);
            HasMany(user => user.Histories).Cascade.Delete().Inverse();

        }
    }
}
