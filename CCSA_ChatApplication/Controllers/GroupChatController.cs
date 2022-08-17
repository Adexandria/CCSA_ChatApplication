using CCSA_ChatApp.Authentication.Services;
using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.Models;
using CCSA_ChatApp.Infrastructure.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CCSA_ChatApplication.Controllers
{
    [Authorize(Roles = "User")]
    [Route("GroupChat")]
    [ApiController]
    public class GroupChatController : ControllerBase
    {

        public GroupChatController(IGroupChatService groupChatService, IAuth authService, ITokenCredential tokenCredential, IUserService userService)
        {
            _groupChatService = groupChatService;
            _authService = authService;
            _tokenCredential = tokenCredential;
            _userService = userService;
        }

        public IGroupChatService _groupChatService { get; }
        public IAuth _authService { get; }
        public ITokenCredential _tokenCredential { get; }
        public IUserService _userService { get; }

        [HttpPost("create-group")]
        public async Task<IActionResult> CreateGroupChat([FromForm]NewGroupChatDTO newGroupChat)
        {
            try
            {
                User user = newGroupChat.Adapt<User>();
                UserProfile userProfile = newGroupChat.Adapt<UserProfile>();

                return Ok();
                //var user = new UserProfile();
                //await _userService.GetUserByUsername(user.Username);


                //await _authService.AddUserRole(new UserRole { Role = "User"});
                //var token = await _tokenCredential.GenerateToken(user.User);
                //await _groupChatService.CreateGroupChat(newGroupChat);
                //return Ok($"{token} with {newGroupChat.GroupName} has been created");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }








    }
}
