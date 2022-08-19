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
            user.Password = EncodePassword(user.Password);
            await _session.SaveAsync(user);
            await Commit();
        }
        
        public override async Task<User> GetUserById(Guid userId)
        {
            return await _session.GetAsync<User>(userId);
        }

        public override async Task<User> GetUserByUsername(string username)
        {
            var user = await _session.Query<User>().FirstOrDefaultAsync(s=>s.Profile.Username == username);
            return user;
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
            else
            {
                throw new Exception("User not found");
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
            else
            {
                throw new Exception("User not found");
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
            else
            {
                throw new Exception("User not found");
            }
        }

        public override async Task UpdatePassword(User user, string oldPassword, string newPassword)
        {
            oldPassword = EncodePassword(oldPassword);
            var currentUser = await VerifyUser(user,oldPassword);
            if (currentUser is not null)
            {
                newPassword = EncodePassword(newPassword);
                currentUser.Password = newPassword;
                await _session.MergeAsync(currentUser);
                await Commit();
            }
            else
            {
                throw new Exception("Password not updated");
            }

        }

        public override async Task<bool> VerifyPassword(string username, string password)
        {
            password = EncodePassword(password);
            var currentUser = await VerifyUser(username,password);
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
            else
            {
                throw new Exception("User not found");
            }
        }


        
        

        private string EncodePassword(string password)
        {
            var encodedPassword = Encoding.UTF8.GetBytes(password);
            password = Convert.ToBase64String(encodedPassword);
            return password;
        }
        
        
        private async Task Commit()
        {
            using var transction = _session.BeginTransaction();
            try
            {
                if (transction.IsActive)
                {
                    await _session.FlushAsync();
                    await transction.CommitAsync();
                    _session.Close();
                }
            }
            catch (Exception ex)
            {
                transction.Rollback();
            }
        }

        
        private async Task<User> VerifyUser(User currentUser, string password)
        {
            if (currentUser.Password == password)
            {
                return currentUser;
            }
            return default;
        }

        private async Task<User> VerifyUser(string username, string password)
        {
            User currentUser = await GetUserByUsername(username);
            if (currentUser is not null)
            {
                if (currentUser.Password == password)
                {
                    return currentUser;
                }
            }
            return default;
        }
    }
}
