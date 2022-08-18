﻿using CCSA_ChatApp.Domain.DTOs.MessageDTOs;
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
        private readonly MessageService _messageService;
        private readonly MessageHistoryService _messageHistoryService;
        public MessageHistoryController(MessageService messageService, MessageHistoryService messageHistoryService)
        {
            _messageService = messageService;
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
        [HttpGet("{username}")]
        public IActionResult FetchMessagesByReceiverUsername(string username)
        {
            var messages = _messageHistoryService.FetchMessagesByReceiverUsername(username);
            return Ok(messages);
        }
      
        [HttpPost("{username}")]
        public async Task<IActionResult> SendMessage([FromBody]string text, string username)
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
        [Authorize(Roles = "GroupUser")]
        [HttpPost("{groupChatId}")]
        public async Task<IActionResult> SendMessageToGroup([FromBody]string text, Guid groupChatId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _messageService.SendMessageToGroup(text, new Guid(userId), groupChatId);
                return Ok("Message sent");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{messageId}")]
        public async Task<IActionResult> UpdateMessageById([FromBody]string text, Guid messageId)
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

        [HttpDelete("{messageid}")]
        public async Task<IActionResult> DeleteMessageByMessageId(Guid messageId)
        {
            await _messageService.DeleteMessageByMessageId(messageId);
            return Ok("Message has been deleted successfully");
        }

    }
}
