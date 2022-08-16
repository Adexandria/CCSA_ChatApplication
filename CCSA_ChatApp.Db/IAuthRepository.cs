using CCSA_ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db
{
    public abstract class IAuthRepository
    {
        public abstract Task AddUserRole(UserRole role);
        public abstract IEnumerable<UserRole> GetUserRole(Guid id);
        public abstract Task SaveRefreshToken(RefreshToken token);
        public abstract Task<RefreshToken> GetExistingToken(Guid userId);
        
    }
}
