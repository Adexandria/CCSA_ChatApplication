using CCSA_ChatApp.Db.Repositories;
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
        
        public async Task<RefreshToken> GetExistingToken(Guid userId,string refreshToken)
        {
            return await _auth.GetExistingToken(userId,refreshToken);
        }

        public IEnumerable<UserRoleDTO> GetUserRole(Guid id)
        {
            var role = _auth.GetUserRole(id);
            var mappedRole = role.Adapt<IEnumerable<UserRoleDTO>>();
            return mappedRole;
        }

        public async Task RemoveUserRole(Guid userId,string groupName)
        {
            await _auth.RemoveUserRole(userId, groupName);
        }

        public async Task RemoveUsersGroupRole(string groupName)
        {
            await _auth.RemoveUsersGroupRole(groupName);
        }

        public async Task SaveRefreshToken(RefreshToken token)
        {
            await _auth.SaveRefreshToken(token);
        }
    }
}
