using CCSA_ChatApp.Db.Repositories;
using CCSA_ChatApp.Domain.DTOs.UserProfileDTOs;
using CCSA_ChatApp.Domain.Models;
using Mapster;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserProfileRepository _userProfileRepository;
        public UserProfileService(UserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public void CreateExistingUserProfile(UserProfile user)
        {
            _userProfileRepository.Create(user);
        }

        public void DeleteUserProfileById(Guid userProfileId)
        {
            _userProfileRepository.DeleteUserProfileById(userProfileId);
        }

        public UserProfileDTO GetUserProfileById(Guid userProfileId)
        {
            var userProfile = _userProfileRepository.GetUserProfileById(userProfileId);
            return userProfile.Adapt<UserProfileDTO>();
        }

        public UserProfileDTO GetUserProfileByUsername(string username)
        {
            var userProfile = _userProfileRepository.GetUserProfileByUsername(username);
            return userProfile.Adapt<UserProfileDTO>();
        }

        public void UpdateCountry(Guid profileId, Country country)
        {
            var userProfile = _userProfileRepository.GetUserProfileById(profileId);
            if(userProfile is not null)
            {
                userProfile.Country = country;
                _userProfileRepository.Update(userProfile);
            }
        }

        public void UpdateUsername(Guid profileId, string username)
        {
            var userProfile = _userProfileRepository.GetUserProfileById(profileId);
            if (userProfile is not null)
            {
                userProfile.Username = username;
                _userProfileRepository.Update(userProfile);
            }
        }

        public void UpdateUserProfilePicture(Guid profileId, byte[] picture)
        {
            var userProfile = _userProfileRepository.GetUserProfileById(profileId);
            if (userProfile is not null)
            {
                userProfile.Picture = picture;
                _userProfileRepository.Update(userProfile);
            }
        }
    }

}
