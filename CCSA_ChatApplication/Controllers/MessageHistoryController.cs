using CCSA_ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CCSA_ChatApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]/messages")]
    [ApiController]
    public class MessageHistoryController : ControllerBase
    {
        private readonly IMessageHistoryService _messageHistoryService;
        public MessageHistoryController(IMessageHistoryService messageHistoryService)
        {
            _messageHistoryService = messageHistoryService;
        }

        [HttpGet]
        public IActionResult FetchMessagesBySenderId()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var messages = _messageHistoryService.FetchMessagesBySenderId(new Guid(userId));
            return Ok(messages);
        }

       
        [HttpGet("{groupId}")]
        public IActionResult FetchGroupChatMessagesByGroupId(Guid groupId)
        {
            
            var messages = _messageHistoryService.FetchGroupChatMessagesByGroupId(groupId);
            return Ok(messages);
        }
        

        //To retrieve messages sent to a user
        [HttpGet("user")]
        public IActionResult FetchMessagesByReceiverUsername(string username)
        {
            var messages = _messageHistoryService.FetchMessagesByReceiverUsername(username);
            return Ok(messages);
        }
        
       

    }
}
