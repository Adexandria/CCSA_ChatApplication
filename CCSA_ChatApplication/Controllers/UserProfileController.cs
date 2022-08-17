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

        public UserProfileController(IUserProfileService userProfileService, IUserService userService)
        {
            _userProfileService = userProfileService;
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            string fullname = User.FindFirstValue(ClaimTypes.Name);
            
            UserDTO currentUser = await _userService.GetUserById(Guid.Parse(userId));
            
            var currentUsers = _userService.GetUsers.Where(s => s.FullName != fullname);
            
            var userProfile = _userProfileService.GetUserProfileById(Guid.Parse(userId));
            
            userProfile.Contacts = currentUsers.ToList();
            
            userProfile.Adapt(currentUser);
            
            //get group chats and map
            
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

            //Get froup chats and map
            return Ok(userProfile);
        }

        [HttpPut("update-username")]
        public async Task<IActionResult> UpdateUsername(string username)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserDTO currentUser = await _userService.GetUserByUsername(username);
            if (currentUser is not null)
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
            _userProfileService.UpdateCountry(Guid.Parse(userId), country);
            return Ok("Updated successfully");
        }

        [HttpPut("update-group-picture")]
        public async Task<IActionResult> UpdatePicture(IFormFile groupPicture)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _userProfileService.UpdateUserPicture(groupPicture, Guid.Parse(userId));
            return Ok("Updated successfully");
        }

        [HttpDelete("delete-group-picture")]
        public async Task<IActionResult> RemovePicture()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _userProfileService.DeleteUserPicture(Guid.Parse(userId));
            return Ok("Removed successfully");
        }
    }
}
