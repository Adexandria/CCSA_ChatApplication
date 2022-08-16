using CCSA_ChatApp.Domain.Models;


namespace CCSA_ChatApp.Db.Repositories
{
    public abstract class IUserRepository
    {
        public abstract Task CreateUser(User user);
        public abstract Task<User> GetUserById(Guid userId);
        public abstract IEnumerable<User> GetUsers { get; }
        public abstract Task UpdateFirstName(string firstName);
        public abstract Task UpdateMiddleName(string middleName);
        public abstract Task UpdateLastName(string lastName);
        public abstract Task UpdatePassword(string oldPassword,string newPassword);
        public abstract Task<bool> VerifyPassword(string password);
        public abstract Task UpdateEmail(string email);
        public abstract Task DeleteByUserId(Guid userId);


    }
}
