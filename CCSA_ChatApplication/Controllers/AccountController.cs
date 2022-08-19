using CCSA_ChatApp.Authentication.Services;
using CCSA_ChatApp.Domain.DTOs;
using CCSA_ChatApp.Domain.DTOs.UserDTOs;
using CCSA_ChatApp.Domain.DTOs.UserProfileDTOs;
using CCSA_ChatApp.Domain.Models;
using CCSA_ChatApp.Infrastructure.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CCSA_ChatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuth _auth;
        private readonly IUserProfileService _userProfileService;
        private readonly ITokenCredential _tokenCredential;


        public AccountController(IUserService userService, IAuth auth, IUserProfileService userProfileService, ITokenCredential tokenCredential )
        {
            _userService = userService;
            _auth = auth;
            _userProfileService = userProfileService;
            _tokenCredential = tokenCredential;
        }
        

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]SignUpDTO newUser)
        {
            //Check if the username exist
            UserProfileDTO currentUser = _userProfileService.GetUserProfileByUsername(newUser.Username);
            if(currentUser is not null)
            {
                return BadRequest("Username exist");
            }
            
            //Map newUser properties into user model
            User user = newUser.Adapt<User>();
            
            //Map newUser properties into user profile model
            UserProfile userProfile = newUser.Adapt<UserProfile>();
            
            //convert image into string
            var image = _userProfileService.ConvertFromImageToByte(newUser.ProfilePicture);
            userProfile.Picture = image;
            
            //Save user
            await _userService.CreateUser(user);

            userProfile.User = user;
            
            //Save user profile
            await _userProfileService.CreateExistingUserProfile(userProfile);

            var token = await _tokenCredential.GenerateToken(user);
            return Ok(new TokenDTO { AccessToken = token });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LogInDTO newUser)
        {
            var verifyPassword = await _userService.VerifyPassword(newUser.UserName,newUser.Password);
            if (verifyPassword)
            {
                var mappedUser = await _userService.GetUserByUsername(newUser.UserName);
                var user = mappedUser.Adapt<User>();
                var token = await  _tokenCredential.GenerateToken(user);
                var refreshToken = await _tokenCredential.GenerateRefreshToken(user);
                return Ok(new TokenDTO { AccessToken = token,RefreshToken = refreshToken.Token});
            }
            return BadRequest("Username or password incorrect");
        }
/*
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> GenerateAccessToken(string refreshToken)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user =  _userService.GetUserById(Guid.Parse(userId)).Result.Adapt<User>();
            var token = await _tokenCredential.GenerateToken(user,refreshToken);
            if (token is not null)
            {
                return Ok(token);
            }
            return Unauthorized();
        }*/

        [HttpGet("password-reset")]
        public async Task<IActionResult> GeneratePasswordResetToken()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var token =  _tokenCredential.GeneratePasswordResetToken(Guid.Parse(userId));
            return Ok(token);
        }

       [HttpPut("password-reset")]
        public async Task<IActionResult> ResetPassword(string token,PasswordDTO passwordReset)
        {
            try
            {

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userProfile = _userProfileService.GetUserProfileById(Guid.Parse(userId));
                var user = _userService.GetUserById(Guid.Parse(userId)).Result.Adapt<User>();
                if (!_tokenCredential.DecodePasswordResetToken(token, Guid.Parse(userId)))
                {
                    return BadRequest("Invalid token");
                }
                await _userService.UpdatePassword(user, passwordReset.OldPassword, passwordReset.NewPassword);
                return Ok("Password Changed");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userService.DeleteByUserId(Guid.Parse(userId));
            return Ok("Account deleted");
        }
    }
}
