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
    public class SendMessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IGroupChatService _groupChatService;
        private readonly IUserService _userService;
        private readonly IMessageHistoryService _messageHistoryService;
        public SendMessageController(IMessageService messageService, IGroupChatService groupChatService, IUserService userService, IMessageHistoryService messageHistoryService)
        {
            _messageService = messageService;
            _groupChatService = groupChatService;
            _userService = userService;
            _messageHistoryService = messageHistoryService;
        }

        [HttpPost("username")]
        public async Task<IActionResult> SendMessage([FromBody] string text, string username)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                var sender =  _userService.GetUserById(new Guid(userId)).Result.Adapt<User>();
                
                var reciever = await _userService.GetUserByUsername(username);
                
                var message = await _messageService.SendMessage(text);
                
                await _messageHistoryService.CreateMessageHistory(message, sender, reciever,null);
                
                return Ok("Message sent");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Policy = "GroupUser")]
        [HttpPost("{groupName}")]
        public async Task<IActionResult> SendMessageToGroup([FromBody] string text, string groupName)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                var group =  _groupChatService.GetGroupChatByName(groupName).Result.Adapt<GroupChat>();
                
                var sender = _userService.GetUserById(new Guid(userId)).Result.Adapt<User>();
                
                if (group is null)
                {
                    return BadRequest("Group does not exist");
                }
           
                var message = await _messageService.SendMessage(text);
                
                await _messageHistoryService.CreateMessageHistory(message, sender,null, group);
                
                return Ok("Message sent");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{messageId}")]
        public async Task<IActionResult> UpdateMessageById([FromBody] string text, Guid messageId)
        {
            try
            {
                await _messageService.UpdateMessageById(text, messageId);
                return Ok("Message Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteMessageByMessageId(Guid messageId)
        {
            await _messageService.DeleteMessageByMessageId(messageId);
            return Ok("Message has been deleted successfully");
        }
    }
}
