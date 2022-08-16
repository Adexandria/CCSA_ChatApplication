using CCSA_ChatApp.Domain.Models;
using NHibernate;
using NHibernate.Linq;
using System.Text;

namespace CCSA_ChatApp.Db.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISession _session;
        public UserRepository(SessionFactory sessionFactory)
        {
            _session = sessionFactory.GetSession();
        }

        
        public override IEnumerable<User> GetUsers
        {
            get
            {
                return _session.Query<User>();
            }
            
        }
        
        public override async Task CreateUser(User user)
        {
            EncodePassword(user.Password);
            await _session.SaveAsync(user);
            await Commit();
        }
        
        public override async Task<User> GetUserById(Guid userId)
        {
            return await _session.GetAsync<User>(userId);
        }
        
        public override async Task UpdateEmail(Guid userId,string email)
        {
            User currentUser = await GetUserById(userId);
            {
                currentUser.Email = email;
                await _session.UpdateAsync(currentUser);
                await Commit();
            }
        }

        public override async Task UpdateFirstName(Guid userId, string firstName)
        {
            User currentUser = await GetUserById(userId);
            if (currentUser is not null)
            {
                currentUser.FirstName = firstName;
                await _session.UpdateAsync(currentUser);
                await Commit();
            }
        }

        public override async Task UpdateLastName(Guid userId, string lastName)
        {
            User currentUser = await GetUserById(userId);
            if (currentUser is not null)
            {
                currentUser.LastName = lastName;
                await _session.UpdateAsync(currentUser);
                await Commit();
            }
        }

        public override async Task UpdateMiddleName(Guid userId, string middleName)
        {
            User currentUser = await GetUserById(userId);
            if (currentUser is not null)
            {
                currentUser.MiddleName = middleName;
                await _session.UpdateAsync(currentUser);
                await Commit();
            }
        }

        public override async Task UpdatePassword(Guid userId, string oldPassword, string newPassword)
        {
            var currentUser = await GetUser(userId,oldPassword);
            if (currentUser is not null)
            {
                EncodePassword(newPassword);
                currentUser.Password = newPassword;
                await _session.UpdateAsync(currentUser);
                await Commit();
            }
        }

        public override async Task<bool> VerifyPassword(Guid userId, string password)
        {
            EncodePassword(password);
            var currentUser = await GetUser(userId,password);
            if (currentUser is not null)
            {
                return true;
            }
            return false;
        }
        
        public override async Task DeleteByUserId(Guid userId)
        {
            User currentUser = await GetUserById(userId);
            if(currentUser is not null)
            {
               await  _session.DeleteAsync(currentUser);
                await Commit();
            }
        }


        
        

        private void EncodePassword(string password)
        {
            var encodedPassword = Encoding.UTF8.GetBytes(password);
            password = Convert.ToBase64String(encodedPassword);
        }
        
        
        private async Task Commit()
        {
            using var transction = _session.BeginTransaction();
            try
            {
                if (transction.IsActive)
                {
                    _session.Flush();
                    await transction.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                transction.Rollback();
            }
        }

        
        private async Task<User> GetUser(Guid userId, string password)
        {
            User currentUser = await GetUserById(userId);
            if (currentUser.Password == password)
            {
                return currentUser;
            }
            return default;
        }
    }
}
