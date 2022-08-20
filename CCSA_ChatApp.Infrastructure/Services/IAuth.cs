using CCSA_ChatApp.Domain.DTOs.UserDTOs;
using CCSA_ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public interface IAuth
    {
        Task AddUserRole(UserRole role);
        IEnumerable<UserRoleDTO> GetUserRole(Guid id);
        IList<List<UsersDTO>> GetRoles(string[] names);
        Task UpdateGroupRoles(string groupName, string updateGroupName);
        Task RemoveUserRole(Guid userId,string groupName);
        Task RemoveUsersGroupRole(string groupName);
        Task SaveRefreshToken(RefreshToken token);
        Task<RefreshToken> GetExistingToken(Guid userId,string refreshToken);
    }
}
