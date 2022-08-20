using CCSA_ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db.Repositories
{
    public abstract class IAuthRepository
    {
        public abstract Task AddUserRole(UserRole role);
        public abstract IEnumerable<UserRole> GetUserRole(Guid id);
        public abstract IEnumerable<UserRole> GetUserRoles(string groupName);
        public abstract IEnumerable<User> GetUser(string groupName);
        public abstract Task UpdateUserGroupRole(UserRole roles);
        public abstract Task RemoveUserRole(Guid userId,string groupName);
        public abstract Task RemoveUsersGroupRole(string groupName);
        public abstract Task SaveRefreshToken(RefreshToken token);
        public abstract Task<RefreshToken> GetExistingToken(Guid userId, string refreshToken);

    }
}
