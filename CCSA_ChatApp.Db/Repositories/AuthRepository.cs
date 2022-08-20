using CCSA_ChatApp.Domain.Models;
using NHibernate;
using NHibernate.Linq;

namespace CCSA_ChatApp.Db.Repositories
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

        public override async Task<RefreshToken> GetExistingToken(Guid userId, string refreshToken)
        {
            var token = await _session.Query<RefreshToken>().FirstOrDefaultAsync(x => x.User.UserId == userId && x.Token == refreshToken);
            return token;
        }

        public override IEnumerable<User> GetUser(string groupName)
        {
            var user = _session.Query<UserRole>().Where(x => x.Role.Contains(groupName)).Select(s=>s.User);
            return user.Distinct();
        }

        public override IEnumerable<UserRole> GetUserRole(Guid id)
        {
            var role = _session.Query<UserRole>().Where(x => x.User.UserId == id);
            return role;
        }

        public override IEnumerable<UserRole> GetUserRoles(string groupName)
        {
            var roles = _session.Query<UserRole>().Where(x => x.Role.Contains(groupName));
            return roles;
        }

        public override async Task RemoveUserRole(Guid userId,string groupName)
        {
            var roles = _session.Query<UserRole>().Where(s => s.User.UserId == userId && s.Role.Contains(groupName));
            foreach (var role in roles)
            {
                await RemoveExistingRole(role);
            }
            
        }

        public override async Task RemoveUsersGroupRole(string groupName)
        {
            var roles = _session.Query<UserRole>().Where(s =>s.Role.Contains(groupName));
            foreach (var role in roles)
            {
                await RemoveExistingRole(role);
            }
        }

        public override async Task SaveRefreshToken(RefreshToken token)
        {
            await _session.SaveAsync(token);
            await Commit();
        }

        public override async Task UpdateUserGroupRole(UserRole role)
        {
            await _session.UpdateAsync(role);
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

        private async Task RemoveExistingRole(UserRole role)
        {
            if (role is not null)
            {
                await _session.DeleteAsync(role);
                await Commit();
            }
            else
            {
                throw new Exception("No group role associated with this user");
            }
        }
    }
}
