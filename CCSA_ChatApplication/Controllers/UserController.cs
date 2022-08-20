using CCSA_ChatApp.Domain.DTOs.UserDTOs;
using CCSA_ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CCSA_ChatApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usersService;

        public UserController(IUserService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("contacts")]
        public IActionResult GetAllContacts()
        {
            try
            {
                string name = User.FindFirstValue(ClaimTypes.Name);

                IEnumerable<UsersDTO> contacts = _usersService.GetUsers(name);

                return Ok(contacts);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut("update-firstname")]
        public async Task<IActionResult> UpdateFirstName(string firstName)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _usersService.UpdateFirstName(Guid.Parse(userId), firstName);

                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPut("update-middlename")]
        public async Task<IActionResult> UpdateMiddleName(string middlename)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _usersService.UpdateMiddleName(Guid.Parse(userId), middlename);

                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPut("update-lastname")]
        public async Task<IActionResult> UpdateLastName(string lastname)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _usersService.UpdateLastName(Guid.Parse(userId), lastname);

                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPut("update-email")]
        public async Task<IActionResult> UpdateEmail(string email)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _usersService.UpdateEmail(Guid.Parse(userId), email);

                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}
