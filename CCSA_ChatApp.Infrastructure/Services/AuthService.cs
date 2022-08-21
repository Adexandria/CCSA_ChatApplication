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

        public IList<List<UsersDTO>> GetRoles(string[] names)
        {
            List<List<UsersDTO>> users = new();

            foreach (var name in names)
            {
                users.Add(GetRole(name));
            }
            return users;
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

        public async Task UpdateGroupRoles(string groupName, string updateGroupName)
        {
            var roles = _auth.GetUserRoles(groupName);
            foreach (var role in roles)
            {
                role.Role.Replace(groupName, updateGroupName);
                await _auth.UpdateUserGroupRole(role);
            }

        }
        public bool GetUserRole(string groupName, Guid userId)
        {
            var users = _auth.GetUser(groupName);
            foreach (var user in users)
            {
                if(user.UserId == userId)
                {
                    return true;
                }
            }
            return false;
        }

        private List<UsersDTO> GetRole(string name)
        {
            var users = _auth.GetUser(name).Adapt<IEnumerable<UsersDTO>>(MappingService.UsersMappingService());
            return users.ToList();
        }

       
    }
}
