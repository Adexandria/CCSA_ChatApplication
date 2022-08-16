using CCSA_ChatApp.Domain.Models;
using NHibernate;
using NHibernate.Linq;

namespace CCSA_ChatApp.Db
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ISession _session;
        public AuthRepository(SessionFactory sessionFactory)
        {
            _session = sessionFactory.GetSession();
        }

        public override async Task AddUserRole(UserRole role)
        {
            await _session.SaveAsync(role);
            await Commit();
        }

        public override async Task<RefreshToken> GetExistingToken(Guid userId)
        {
            var token = await _session.Query<RefreshToken>().FirstOrDefaultAsync(x => x.User.UserId == userId);
            return token;
        }
        
        public override IEnumerable<UserRole> GetUserRole(Guid id)
        {
            var role = _session.Query<UserRole>().Where(x => x.User.UserId == id);
            return role;
        }
        
        public override async Task SaveRefreshToken(RefreshToken token)
        {
            await _session.SaveAsync(token);
            await Commit();
        }

        private async Task Commit()
        {
            try
            {
                var transaction = _session.BeginTransaction();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
