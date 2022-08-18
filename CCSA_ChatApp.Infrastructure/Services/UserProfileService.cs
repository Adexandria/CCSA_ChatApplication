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

        public async Task CreateExistingUserProfile(UserProfile user)
        {
            await _userProfileRepository.Create(user);
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

        public async Task UpdateCountry(Guid profileId, Country country)
        {
            var userProfile = _userProfileRepository.GetUserProfileById(profileId);
            if(userProfile is not null)
            {
                userProfile.Country = country;
                await _userProfileRepository.Update(userProfile);
            }
        }

        public async Task UpdateUsername(Guid profileId, string username)
        {
            var userProfile = _userProfileRepository.GetUserProfileById(profileId);
            if (userProfile is not null)
            {
                userProfile.Username = username;
                await _userProfileRepository.Update(userProfile);
            }
        }

        public async Task UpdateUserPicture(IFormFile picture, Guid userId)
        {
            var user = _userProfileRepository.GetUserProfileById(userId);
            var image = ConvertFromImageToByte(picture);
            user.Picture = image;
            await _userProfileRepository.Update(user);
        }

        public async Task DeleteUserPicture(Guid userId)
        {
            var user = _userProfileRepository.GetUserProfileById(userId);
            user.Picture = null;
            await _userProfileRepository.Update(user);
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
