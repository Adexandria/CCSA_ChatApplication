using CCSA_ChatApp.Domain.DTOs.UserDTOs;
using CCSA_ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public interface IUserService
    {
         Task CreateUser(User user);
         Task<UserDTO> GetUserById(Guid userId);
        IEnumerable<UsersDTO> GetUsers { get; }
         Task UpdateFirstName(Guid userId, string firstName);
         Task UpdateMiddleName(Guid userId, string middleName);
         Task UpdateLastName(Guid userId, string lastName);
         Task UpdatePassword(Guid userId, string oldPassword, string newPassword);
         Task<bool> VerifyPassword(Guid userId, string password);
         Task UpdateEmail(Guid userId, string email);
         Task DeleteByUserId(Guid userId);
    }
}
