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
        public async Task<IActionResult> CreateGroupChat(NewGroupChatDTO newGroupChat, string accessToken)
        {
            try
            {
                await _groupChatService.CreateGroupChat(newGroupChat);
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _userService.GetUserById(Guid.Parse(userId)).Result.Adapt<User>();
                var token = await _tokenCredential.GenerateToken(user, accessToken);
                return Ok(token.AccessToken);
                //return Ok($"{newGroupChat.GroupName} Created");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }





    }
}
