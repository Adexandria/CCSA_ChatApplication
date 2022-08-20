using CCSA_ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>
    {
        public UserProfileRepository(SessionFactory sessionFactory) : base(sessionFactory)
        {

        }

        public UserProfile? GetUserProfileById(Guid userProfileId)
        {
            var userProfile = _session.Query<UserProfile>().FirstOrDefault(x => x.User.UserId == userProfileId);
            return userProfile;
        }

        public UserProfile GetUserProfileByUsername(string username)
        {
            var userProfile = _session.Query<UserProfile>().FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
            return userProfile;
        }

        public void DeleteUserProfileById(Guid userProfileId)
        {
            var userProfile = GetUserProfileById(userProfileId);
            if (userProfile != null)
            {
                _session.Delete(userProfileId);
                Commit();
            }
            else
            {
                throw new Exception("User profile not found");
            }
        }
    }
}
