
using CCSA_ChatApp.Domain.Models;
using FluentNHibernate.Mapping;

namespace CCSA_ChatApp.Db.Mappings
{
    public class UserProfileMap :ClassMap<UserProfile>
    {
        public UserProfileMap()
        {
            Table("UserProfile");
            Id(s => s.ProfileId).GeneratedBy.Guid() ;
            Map(s => s.Country).CustomType<Country>();
            Map(s => s.Username);
            Map(s => s.Picture);
            HasOne(s => s.User);
        }
    }
}
