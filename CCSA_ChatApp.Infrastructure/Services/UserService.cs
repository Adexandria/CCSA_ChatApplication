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

        public async Task<UserDTO> GetUserByUsername(string username)
        {
            User user = await _userRepository.GetUserByUsername(username);
            return user.Adapt<UserDTO>();
        }
        public async Task UpdateEmail(Guid userId,string email)
        {
            await _userRepository.UpdateEmail(userId,email);
        }

        public async Task UpdateFirstName(Guid userId, string firstName)
        {
            await _userRepository.UpdateFirstName(userId,firstName);
        }

        public async Task UpdateLastName(Guid userId, string lastName)
        {
            await _userRepository.UpdateLastName(userId,lastName);
        }

        public async Task UpdateMiddleName(Guid userId, string middleName)
        {
            await _userRepository.UpdateMiddleName(userId,middleName);
        }
        
        public async Task UpdatePassword(User user, string oldPassword, string newPassword)
        {
            await _userRepository.UpdatePassword(user,oldPassword, newPassword);
        }

        public async Task DeleteByUserId(Guid userId)
        {
           await  _userRepository.DeleteByUserId(userId);
        }

        public async Task<bool> VerifyPassword(string username, string password)
        {
            return await _userRepository.VerifyPassword(username,password);
        }

       
    }
}
