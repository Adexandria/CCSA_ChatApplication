using CCSA_ChatApp.Domain.Models;
using FluentNHibernate.Mapping;


namespace CCSA_ChatApp.Db.Mappings
{
    public class UserRoleMap : ClassMap<UserRole>
    {
        public UserRoleMap()
        {
            Table("UserRole");
            Id(s => s.RoleId).GeneratedBy.Guid();
            Map(s => s.Role);
            References(s => s.User);/*.Cascade.All();*/
        }
    }
}
