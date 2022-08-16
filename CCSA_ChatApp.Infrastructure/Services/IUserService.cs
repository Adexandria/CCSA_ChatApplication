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
         Task UpdateFirstName(string firstName);
         Task UpdateMiddleName(string middleName);
         Task UpdateLastName(string lastName);
         Task UpdatePassword(string oldPassword, string newPassword);
         Task<bool> VerifyPassword(string password);
         Task UpdateEmail(string email);
         Task DeleteByUserId(Guid userId);
    }
}
