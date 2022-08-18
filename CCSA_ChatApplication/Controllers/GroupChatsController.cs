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
    public class GroupChatsController : ControllerBase
    {

        public GroupChatsController(IGroupChatService groupChatService, IAuth authService, ITokenCredential tokenCredential, IUserService userService)
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


        [HttpGet]
        public IActionResult GetGroupChats()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var groupChats = _groupChatService.GetAll(Guid.Parse(userId));
            return Ok(groupChats);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGroupChat([FromForm] GroupChatCreateDTO newGroupChat)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                User currentUser = _userService.GetUserById(Guid.Parse(userId)).Result.Adapt<User>();
                
                var image = _groupChatService.ConvertFromImageToByte(newGroupChat.Picture);
                
                GroupChat groupChat = newGroupChat.Adapt<GroupChat>();
                
                await _groupChatService.CreateGroupChat(groupChat);
                
                await _authService.AddUserRole(new UserRole { Role = $"{newGroupChat.GroupName}Admin" });
                
                var token = await _tokenCredential.GenerateToken(currentUser);
                
                return Ok(new { token});
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "GroupAdmin")]
        [HttpPost("{groupChatId}/add-user")]
        public async Task<IActionResult> AddUserToGroup(Guid groupChatId,string username)
        {
            var currentUser =  _userService.GetUserByUsername(username).Result.Adapt<User>();
            if(currentUser is null)
            {
                return NotFound("User doesn't exist");
            }
            var groupChat = _groupChatService.GetGroupChat(groupChatId).Result.Adapt<GroupChat>();
            if (groupChat is null)
            {
                return NotFound("Group not found");
            }
            await _groupChatService.AddUserToGroup(groupChatId, currentUser);
            return Ok("Added Successfully");
        }
        

        [Authorize(Policy = "GroupAdmin")]
        [HttpPut("{groupChatId}/update-picture")]
        public async Task<IActionResult> UpdateGroupPicture(Guid groupChatId, IFormFile picture)
        {
            try
            {
                var groupChat = _groupChatService.GetGroupChat(groupChatId).Result.Adapt<GroupChat>();
                if (groupChat is null)
                {
                    return NotFound();
                }
                await _groupChatService.UpdateGroupPicture(picture, groupChat);
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "GroupAdmin")]
        [HttpPut("{groupChatId}/update-name")]
        public async Task<IActionResult> UpdateGroupName(Guid groupChatId, string name)
        {
            try
            {
                var groupChat = _groupChatService.GetGroupChat(groupChatId).Result.Adapt<GroupChat>();
                if(groupChat is null)
                {
                    return NotFound();
                }
                await _groupChatService.UpdateGroupName(groupChatId, name);
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "GroupAdmin")]
        [HttpPut("{groupChatId}/update-description")]
        public async Task<IActionResult> UpdateGroupDescription(Guid groupChatId, string description)
        {
            try
            {
                var groupChat = _groupChatService.GetGroupChat(groupChatId).Result.Adapt<GroupChat>();
                if (groupChat is null)
                {
                    return NotFound();
                }
                await _groupChatService.UpdateGroupDescription(groupChatId, description);
                return Ok("Name Updated scuuessfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        

        [Authorize(Policy = "GroupAdmin")]
        [HttpDelete("{groupChatId}")]
        public async Task<IActionResult> DeleteGroupChatById(Guid groupChatId)
        {
            var groupChat = _groupChatService.GetGroupChat(groupChatId).Result.Adapt<GroupChat>();
            if (groupChat is null)
            {
                return NotFound();
            }
            await _groupChatService.DeleteGroupChatById(groupChatId);
            return Ok("Successful");
        }

        [Authorize(Policy = "GroupAdmin")]
        [HttpDelete("{groupChatId}/remove-user")]
        public async Task<IActionResult> RemoveUserFromGroupChat(Guid groupChatId,string username)
        {
            var currentUser = _userService.GetUserByUsername(username).Result.Adapt<User>();
            if (currentUser is null)
            {
                return NotFound("User doesn't exist");
            }
            var groupChat = _groupChatService.GetGroupChat(groupChatId).Result.Adapt<GroupChat>();
            if (groupChat is null)
            {
                return NotFound("Group not found");
            }
            await _groupChatService.RemoveUserToGroup(groupChatId, currentUser);
            return Ok("Added Successfully");
        }

        [HttpDelete("{groupChatId}/remove-user")]
        public async Task<IActionResult> RemoveUserFromGroupChat(Guid groupChatId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = _userService.GetUserById(Guid.Parse(userId)).Result.Adapt<User>();
            if (currentUser is null)
            {
                return NotFound("User doesn't exist");
            }
            var groupChat = _groupChatService.GetGroupChat(groupChatId).Result.Adapt<GroupChat>();
            if (groupChat is null)
            {
                return NotFound("Group not found");
            }
            await _groupChatService.RemoveUserToGroup(groupChatId, currentUser);
            return Ok("Added Successfully");
        }
    }
}
