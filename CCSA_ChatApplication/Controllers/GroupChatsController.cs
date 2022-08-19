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
    [Route("api/[controller]")]
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
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var groupChats = _groupChatService.GetAll(Guid.Parse(userId));
                return Ok(groupChats);
            }
            catch (Exception e)
            {

                return BadRequest("Please log in with your correct token");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGroupChat([FromForm] GroupChatCreateDTO newGroupChat)
        {
            try
            {
                var currentGroup = await _groupChatService.GetGroupChatByName(newGroupChat.GroupName);
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                User currentUser = _userService.GetUserById(Guid.Parse(userId)).Result.Adapt<User>();

                var image = _groupChatService.ConvertFromImageToByte(newGroupChat.GroupPicture);

                GroupChat groupChat = newGroupChat.Adapt<GroupChat>();

                groupChat.Picture = image;

                groupChat.CreatedBy = currentUser;

                await _groupChatService.CreateGroupChat(groupChat);

                await _authService.AddUserRole(new UserRole { Role = $"{newGroupChat.GroupName}Admin", User = currentUser });

                var token = await _tokenCredential.GenerateToken(currentUser);

                return Ok(new { token });
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "GroupAdmin")]
        [HttpPost("{groupName}/add-user")]
        public async Task<IActionResult> AddUserToGroup(string groupName, string username)
        {
            try
            {
                var currentUser = _userService.GetUserByUsername(username).Result.Adapt<User>();
                if (currentUser is null)
                {
                    return NotFound("User doesn't exist");

                }

                var groupChat =  _groupChatService.GetGroupChatByName(groupName).Result.Adapt<GroupChat>();
                if (groupChat is null)
                {
                    return NotFound("Group not found");
                }

                await _groupChatService.AddUserToGroup(groupChat.GroupId, currentUser);
                return Ok("Added Successfully");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [Authorize(Policy = "GroupAdmin")]
        [HttpPut("{groupName}/update-picture")]
        public async Task<IActionResult> UpdateGroupPicture(string groupName, IFormFile picture)
        {
            try
            {
                var groupChat = _groupChatService.GetGroupChatByName(groupName).Result.Adapt<GroupChat>();
                //if (groupChat is null)
                //{
                //    return NotFound();
                //}
                await _groupChatService.UpdateGroupPicture(picture, groupChat);
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [Authorize(Policy = "GroupAdmin")]
        [HttpPut("{groupName}/update-name")]
        public async Task<IActionResult> UpdateGroupName(string groupName, string name)
        {
            try
            {
                var groupChat = _groupChatService.GetGroupChatByName(groupName).Result.Adapt<GroupChat>();
                if (groupChat is null)
                {
                    return NotFound();
                }
                await _groupChatService.UpdateGroupName(groupChat.GroupId, name);
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [Authorize(Policy = "GroupAdmin")]
        [HttpPut("{groupName}/update-description")]
        public async Task<IActionResult> UpdateGroupDescription(string groupName, string description)
        {
            try
            {
                var groupChat = _groupChatService.GetGroupChatByName(groupName).Result.Adapt<GroupChat>();
                if (groupChat is null)
                {
                    return NotFound();
                }
                await _groupChatService.UpdateGroupDescription(groupChat.GroupId, description);
                return Ok("Name Updated scuuessfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [Authorize(Policy = "GroupAdmin")]
        [HttpDelete("{groupName}")]
        public async Task<IActionResult> DeleteGroupChatById(string groupName)
        {
            var groupChat = _groupChatService.GetGroupChatByName(groupName).Result.Adapt<GroupChat>();
            if (groupChat is null)
            {
                return NotFound();
            }
            await _groupChatService.DeleteGroupChatById(groupChat.GroupId);
            return Ok("Successful");
        }


        [Authorize(Policy = "GroupAdmin")]
        [HttpDelete("{groupName}/remove-user")]
        public async Task<IActionResult> RemoveUserFromGroupChat(string groupName, string username)
        {
            var currentUser = _userService.GetUserByUsername(username).Result.Adapt<User>();
            if (currentUser is null)
            {
                return NotFound("User doesn't exist");
            }
            var groupChat = _groupChatService.GetGroupChatByName(groupName).Result.Adapt<GroupChat>();
            if (groupChat is null)
            {
                return NotFound("Group not found");
            }
            await _groupChatService.RemoveUserToGroup(groupChat.GroupId, currentUser);
            return Ok("Added Successfully");
        }

        [HttpDelete("{groupName}/remove-user")]
        public async Task<IActionResult> RemoveUserFromGroupChat(string groupName)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = _userService.GetUserById(Guid.Parse(userId)).Result.Adapt<User>();
            if (currentUser is null)
            {
                return NotFound("User doesn't exist");
            }
            var groupChat = _groupChatService.GetGroupChatByName(groupName).Result.Adapt<GroupChat>();
            if (groupChat is null)
            {
                return NotFound("Group not found");
            }
            await _groupChatService.RemoveUserToGroup(groupChat.GroupId, currentUser);
            return Ok("Added Successfully");
        }
    }
}
