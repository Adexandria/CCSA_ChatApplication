using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.Models;
using CCSA_ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCSA_ChatApplication.Controllers
{
    [Authorize(Roles = "User")]
    [Route("GroupChat")]
    [ApiController]
    public class GroupChatController : ControllerBase
    {

        public GroupChatController(IGroupChatService groupChatService, IAuth authService)
        {
            _groupChatService = groupChatService;
            _authService = authService;
        }

        public IGroupChatService _groupChatService { get; }
        public IAuth _authService { get; }


        [HttpPost("create-group")]
        public async Task<IActionResult> CreateGroupChat([FromBody] GroupChatDTO newGroupChat)
        {
            await _groupChatService.CreateGroupChat(newGroupChat.GroupName,newGroupChat.GroupDescription,newGroupChat.Picture);
            return Ok($"{ newGroupChat.GroupName} Created");
        }
    }
}
