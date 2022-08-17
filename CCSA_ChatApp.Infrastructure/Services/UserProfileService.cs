using CCSA_ChatApp.Db.Repositories;
using CCSA_ChatApp.Domain.DTOs.UserProfileDTOs;
using CCSA_ChatApp.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;

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
            var mappedUserProfile = userProfile.Adapt<UserProfileDTO>();
            /*mappedUserProfile.Picture = ConvertFronByteToString(userProfile.Picture);*/
            return mappedUserProfile;
        }

        public UserProfileDTO GetUserProfileByUsername(string username)
        {
            var userProfile = _userProfileRepository.GetUserProfileByUsername(username);
            var mappedUserProfile = userProfile.Adapt<UserProfileDTO>();
            /*mappedUserProfile.Picture = ConvertFronByteToString(userProfile.Picture);*/
            return mappedUserProfile;
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

        public void UpdateUserPicture(IFormFile picture, Guid userId)
        {
            var user = _userProfileRepository.GetUserProfileById(userId);
            var image = ConvertFromImageToByte(picture);
            user.Picture = image;
            _userProfileRepository.Update(user);
        }

        public void DeleteUserPicture(Guid userId)
        {
            var user = _userProfileRepository.GetUserProfileById(userId);
            user.Picture = null;
            _userProfileRepository.Update(user);
        }


        public byte[] ConvertFromImageToByte(IFormFile picture)
        {
            if (picture.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    picture.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return fileBytes;
                }
            }
            return default;
        }

        private string ConvertFronByteToString(byte[] picture)
        {
            if (picture is not null)
            {
                return Convert.ToBase64String(picture);
            }
            return default;
        }

    }

}
