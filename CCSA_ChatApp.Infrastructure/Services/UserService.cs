using CCSA_ChatApp.Db.Repositories;
using CCSA_ChatApp.Domain.DTOs.UserDTOs;
using CCSA_ChatApp.Domain.Models;
using Mapster;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IEnumerable<UsersDTO> GetUsers
        {
            get
            {
                IEnumerable<User> users = _userRepository.GetUsers;
                return users.Adapt<IEnumerable<UsersDTO>>();
            }
        }

        public async Task CreateUser(User user)
        {
           await  _userRepository.CreateUser(user);
        }

        
        public async Task<UserDTO> GetUserById(Guid userId)
        {
           User user = await _userRepository.GetUserById(userId);
            return user.Adapt<UserDTO>();
        }

        public async Task UpdateEmail(string email)
        {
            await _userRepository.UpdateEmail(email);
        }

        public async Task UpdateFirstName(string firstName)
        {
            await _userRepository.UpdateFirstName(firstName);
        }

        public async Task UpdateLastName(string lastName)
        {
            await _userRepository.UpdateLastName(lastName);
        }

        public async Task UpdateMiddleName(string middleName)
        {
            await _userRepository.UpdateMiddleName(middleName);
        }

        public async Task UpdatePassword(string oldPassword, string newPassword)
        {
            await _userRepository.UpdatePassword(oldPassword, newPassword);
        }

        public async Task DeleteByUserId(Guid userId)
        {
           await  _userRepository.DeleteByUserId(userId);
        }

        public async Task<bool> VerifyPassword(string password)
        {
            return await _userRepository.VerifyPassword(password);
        }
    }
}
