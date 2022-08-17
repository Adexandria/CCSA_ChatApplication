using CCSA_ChatApp.Domain.DTOs.UserProfileDTOs;
using CCSA_ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public interface IUserProfileService
    {
        UserProfileDTO GetUserProfileById(Guid userProfileId);
        void CreateExistingUserProfile(UserProfile user);
        void DeleteUserProfileById(Guid userProfileId);
        UserProfileDTO GetUserProfileByUsername(string username);
        void UpdateUserProfilePicture(Guid profileId, string picture);
        void UpdateUsername(Guid profileId, string username);
        void UpdateCountry(Guid profileId, Country country);
    }
}
