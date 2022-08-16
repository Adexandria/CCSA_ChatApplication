using CCSA_ChatApp.Domain.Models;


namespace CCSA_ChatApp.Db.Repositories
{
    public abstract class IUserRepository
    {
        public abstract Task CreateUser(User user);
        public abstract Task<User> GetUserById(Guid userId);
        public abstract IEnumerable<User> GetUsers { get; }
        public abstract Task UpdateFirstName(Guid userId, string firstName);
        public abstract Task UpdateMiddleName(Guid userId, string middleName);
        public abstract Task UpdateLastName(Guid userId, string lastName);
        public abstract Task UpdatePassword(Guid userId, string oldPassword,string newPassword);
        public abstract Task<bool> VerifyPassword(Guid userId, string password);
        public abstract Task UpdateEmail(Guid userId, string email);
        public abstract Task DeleteByUserId(Guid userId);


    }
}
