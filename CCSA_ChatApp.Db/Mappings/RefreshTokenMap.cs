using CCSA_ChatApp.Domain.Models;
using FluentNHibernate.Mapping;

namespace CCSA_ChatApp.Db.Mappings
{
    public class RefreshTokenMap :ClassMap<RefreshToken>
    {
        public RefreshTokenMap()
        {
            Id(x => x.TokenId).GeneratedBy.GuidComb();
            Map(x => x.Token);
            Map(x => x.ExpiryDate);
            References(x => x.User);
        }
    }
}
