using CCSA_ChatApp.Domain.DTOs.UserProfileDTOs;
using CCSA_ChatApp.Domain.Models;
using Microsoft.AspNetCore.Http;
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
        Task CreateExistingUserProfile(UserProfile user);
        void DeleteUserProfileById(Guid userProfileId);
        UserProfileDTO GetUserProfileByUsername(string username);
        Task UpdateUserPicture(IFormFile picture, Guid userId);
        Task DeleteUserPicture(Guid userId);
        Task UpdateUsername(Guid profileId, string username);
        Task UpdateCountry(Guid profileId, Country country);
        byte[] ConvertFromImageToByte(IFormFile picture);
    }
}
