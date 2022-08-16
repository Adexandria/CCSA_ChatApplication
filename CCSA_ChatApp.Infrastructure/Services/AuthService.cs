using CCSA_ChatApp.Db;
using CCSA_ChatApp.Domain.DTOs.UserDTOs;
using CCSA_ChatApp.Domain.Models;
using Mapster;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public class AuthService : IAuth
    {
        private AuthRepository _auth;
        public AuthService(AuthRepository auth)
        {
            _auth = auth;
        }
        public async Task AddUserRole(UserRole role)
        {
            await _auth.AddUserRole(role);
        }
        
        public async Task<RefreshToken> GetExistingToken(Guid userId)
        {
            return await _auth.GetExistingToken(userId);
        }

        public IEnumerable<UserRoleDTO> GetUserRole(Guid id)
        {
            var role = _auth.GetUserRole(id);
            var mappedRole = role.Adapt<IEnumerable<UserRoleDTO>>();
            return mappedRole;
        }

        public async Task SaveRefreshToken(RefreshToken token)
        {
            await _auth.SaveRefreshToken(token);
        }
    }
}
