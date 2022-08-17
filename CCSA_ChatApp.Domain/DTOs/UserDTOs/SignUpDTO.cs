using CCSA_ChatApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CCSA_ChatApp.Domain.DTOs.UserDTOs
{
    public class SignUpDTO
    {
        [Required(ErrorMessage = "Enter First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Enter Lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = true)]
        public Country Country { get; set; } = Country.Nigeria;

        [Required]
        [Password, StringLength(7, ErrorMessage = "Password must be more than 4 and less than 7 characters", MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword do not match")]
        public string ConfirmPassword { get; set; }


        [Required(AllowEmptyStrings = true)]
        public IFormFile ProfilePicture { get; set; }

    }
}
