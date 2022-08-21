using CCSA_ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public SendMessageController(IMessageService messageService, IGroupChatService groupChatService)
        {
            _messageService = messageService;
            _groupChatService = groupChatService;
        }

        [HttpPost("username")]
        public async Task<IActionResult> SendMessage([FromBody] string text, string username)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _messageService.SendMessage(text, new Guid(userId), username);
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
                var group = await _groupChatService.GetGroupChatByName(groupName);
                if(group is null)
                {
                    return BadRequest("Group does not exist");
                }
           
                await _messageService.SendMessageToGroup(text, Guid.Parse(userId), group.GroupId);
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


        //[HttpDelete("{messageId}")]
        //public async Task<IActionResult> DeleteMessageByMessageId(Guid messageId)
        //{
        //    await _messageService.DeleteMessageByMessageId(messageId);
        //    return Ok("Message has been deleted successfully");
        //}
    }
}
