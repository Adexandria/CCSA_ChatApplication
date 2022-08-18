using CCSA_ChatApp.Domain.DTOs.UserDTOs;
using CCSA_ChatApp.Domain.DTOs.UserProfileDTOs;
using CCSA_ChatApp.Domain.Models;
using CCSA_ChatApp.Infrastructure.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CCSA_ChatApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IUserService _userService;
        private readonly IGroupChatService _groupChatService;

        public UserProfileController(IUserProfileService userProfileService, IUserService userService, IGroupChatService groupChatService)
        {
            _userProfileService = userProfileService;
            _userService = userService;
            _groupChatService = groupChatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            string fullname = User.FindFirstValue(ClaimTypes.Name);
            
            UserDTO currentUser = await _userService.GetUserById(Guid.Parse(userId));
            
            var currentUsers = _userService.GetUsers(fullname);

            var userProfile = currentUser.Adapt<UserProfileDTO>(MappingService.UserProfileMappingService());
            
            userProfile.Contacts = currentUsers.ToList();
            
            userProfile.Adapt(currentUser);

            userProfile.GroupChats = _groupChatService.GetAll(Guid.Parse(userId)).ToList();

            return Ok(userProfile);
            
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserprofileByUsername(string username)
        {
            var userProfile = _userProfileService.GetUserProfileByUsername(username).Adapt<UserProfilesDTO>();
            if (userProfile is null)
            {
                return NotFound("This user doesn't exist");
            }

            return Ok(userProfile);
        }

        [HttpPut("update-username")]
        public async Task<IActionResult> UpdateUsername(string username)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser =  _userProfileService.GetUserProfileByUsername(username);
            if (currentUser is null)
            {
                _userProfileService.UpdateUsername(Guid.Parse(userId), username);
                return Ok("Updated successfully");
            }
            return BadRequest("This username is already taken");
        }

        [HttpPut("update-country")]
        public async Task<IActionResult> UpdateCountry(Country country)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userProfileService.UpdateCountry(Guid.Parse(userId), country);
            return Ok("Updated successfully");
        }

        [HttpPut("update-picture")]
        public async Task<IActionResult> UpdatePicture(IFormFile picture)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userProfileService.UpdateUserPicture(picture, Guid.Parse(userId));
            return Ok("Updated successfully");
        }

        [HttpDelete("delete-picture")]
        public async Task<IActionResult> RemovePicture()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userProfileService.DeleteUserPicture(Guid.Parse(userId));
            return Ok("Removed successfully");
        }
    }
}
