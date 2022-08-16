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
        
        public override async Task UpdateEmail(string email)
        {
            User currentUser = await _session.Query<User>().Where(x => x.Email.Equals(email)).FirstOrDefaultAsync();
            if(currentUser is not null)
            {
                currentUser.Email = email;
                await _session.UpdateAsync(currentUser);
                await Commit();
            }
        }

        public override async Task UpdateFirstName(string firstName)
        {
            User currentUser = await _session.Query<User>().Where(x => x.FirstName.Equals(firstName)).FirstOrDefaultAsync();
            if (currentUser is not null)
            {
                currentUser.Email = firstName;
                await _session.UpdateAsync(currentUser);
                await Commit();
            }
        }

        public override async Task UpdateLastName(string lastName)
        {
            User currentUser = await _session.Query<User>().Where(x => x.LastName.Equals(lastName)).FirstOrDefaultAsync();
            if (currentUser is not null)
            {
                currentUser.LastName = lastName;
                await _session.UpdateAsync(currentUser);
                await Commit();
            }
        }

        public override async Task UpdateMiddleName(string middleName)
        {
            User currentUser = await _session.Query<User>().Where(x => x.MiddleName.Equals(middleName)).FirstOrDefaultAsync();
            if (currentUser is not null)
            {
                currentUser.MiddleName = middleName;
                await _session.UpdateAsync(currentUser);
                await Commit();
            }
        }

        public override async Task UpdatePassword(string oldPassword, string newPassword)
        {
            var currentUser = await GetUser(oldPassword);
            if (currentUser is not null)
            {
                EncodePassword(newPassword);
                currentUser.Password = newPassword;
                await _session.UpdateAsync(currentUser);
                await Commit();
            }
        }

        public override async Task<bool> VerifyPassword(string password)
        {
            EncodePassword(password);
            var currentUser = await GetUser(password);
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

        
        private async Task<User> GetUser(string password)
        {
            User currentUser = await _session.Query<User>().Where(x => x.Password.Equals(password)).FirstOrDefaultAsync();
            return currentUser;
        }
    }
}
