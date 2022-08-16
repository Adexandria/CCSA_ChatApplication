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
        Task SaveRefreshToken(RefreshToken token);
        Task<RefreshToken> GetExistingToken(Guid userId);
    }
}
