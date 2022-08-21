using CCSA_ChatApp.Authentication.Services;
using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.DTOs.UserDTOs;
using CCSA_ChatApp.Domain.Models;
using CCSA_ChatApp.Infrastructure.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CCSA_ChatApplication.Controllers
{
    [Authorize]
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
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            List<GroupChatsDTO> groupChats = _groupChatService.GetAll(Guid.Parse(userId)).ToList();
            
            string[] groupNames = groupChats.Select(s => s.GroupName).ToArray();
            
            IList<List<UsersDTO>> members = _authService.GetRoles(groupNames);

           MappingService.MapUserToGroupMembers(groupChats, members);
            
            return Ok(groupChats);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGroupChat([FromForm] GroupChatCreateDTO newGroupChat)
        {
            try
            {
                
                var currentGroup = await _groupChatService.GetGroupChatByName(newGroupChat.GroupName);
                if(currentGroup != null)
                {
                    return BadRequest("GroupName already exist");
                }
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                User currentUser = _userService.GetUserById(Guid.Parse(userId)).Result.Adapt<User>(MappingService.UserDTOMappingService());
                
                var image = _groupChatService.ConvertFromImageToByte(newGroupChat.GroupPicture);
                
                GroupChat groupChat = newGroupChat.Adapt<GroupChat>();

                groupChat.Picture = image;
                
                groupChat.CreatedBy = currentUser;
                
                await _groupChatService.CreateGroupChat(groupChat);
                
                await _authService.AddUserRole(new UserRole { Role = $"{newGroupChat.GroupName}Admin" , User = currentUser});
                
                await _authService.AddUserRole(new UserRole { Role = $"{newGroupChat.GroupName}User", User = currentUser });

                var token = await _tokenCredential.GenerateToken(currentUser);

                return Ok(new { token});
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "GroupAdmin")]
        [HttpPost("{groupName}/add-admin")]
        public async Task<IActionResult> AddAdmin(string groupName, string username)
        {
            try
            {
                var user = await _userService.GetUserByUsername(username);
                if (user == null)
                {
                    return BadRequest("User does not exist");
                }
                bool isMember = _authService.GetUserRole(groupName,user.UserId);
                if (isMember)
                {
                    await _authService.AddUserRole(new UserRole { Role = $"{groupName}Admin", User = user });
                    return Ok($"Added {username} as an admin");
                }                
                return BadRequest("User is not a member of this group");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "GroupAdmin")]
        [HttpPost("{groupName}/add-user")]
        public async Task<IActionResult> AddUserToGroup(string groupName,string username)
        {
            var currentUser =  await _userService.GetUserByUsername(username);
            if(currentUser is null)
            {
                return NotFound("User doesn't exist");
                
            }
            
            var groupChat = await _groupChatService.GetGroupChatByName(groupName);
            if (groupChat is null)
            {
                return NotFound("Group not found");
            }
            await _groupChatService.AddUserToGroup(groupChat.GroupId, currentUser);
            await _authService.AddUserRole(new UserRole { Role = $"{groupChat.GroupName}User", User = currentUser });
            return Ok("Added Successfully");
        }
        

        [Authorize(Policy = "GroupAdmin")]
        [HttpPut("{groupName}/update-picture")]
        public async Task<IActionResult> UpdateGroupPicture(string groupName, IFormFile picture)
        {
            try
            {
                var groupChat = await _groupChatService.GetGroupChatByName(groupName);
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
        [HttpPut("{groupName}/update-name")]
        public async Task<IActionResult> UpdateGroupName(string groupName, string name)
        {
            try
            {
                var groupChat = await _groupChatService.GetGroupChatByName(groupName);
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
                var groupChat = await _groupChatService.GetGroupChatByName(groupName);
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
        [HttpDelete("{groupName}/remove-user")]
        public async Task<IActionResult> RemoveUserFromGroupChat(string groupName,string username)
        {
            var currentUser = await _userService.GetUserByUsername(username);
            if (currentUser is null)
            {
                return NotFound("User doesn't exist");
            }
            var groupChat = await _groupChatService.GetGroupChatByName(groupName);
            if (groupChat is null)
            {
                return NotFound("Group not found");
            }
            await _authService.RemoveUserRole(currentUser.UserId, groupName);
            await _groupChatService.RemoveUserFromGroup(groupChat.GroupId, currentUser);
            return Ok("Removed Successfully");
        }

        [HttpDelete("{groupName}/remove")]
        public async Task<IActionResult> RemoveUserFromGroupChat(string groupName)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = _userService.GetUserById(Guid.Parse(userId)).Result.Adapt<User>();
            if (currentUser is null)
            {
                return NotFound("User doesn't exist");
            }
            var groupChat = await _groupChatService.GetGroupChatByName(groupName);
            if (groupChat is null)
            {
                return NotFound("Group not found");
            }
            await _authService.RemoveUserRole(currentUser.UserId, groupName);
            await _groupChatService.RemoveUserFromGroup(groupChat.GroupId, currentUser);
            return Ok("Removed Successfully");
        }

    }
}
