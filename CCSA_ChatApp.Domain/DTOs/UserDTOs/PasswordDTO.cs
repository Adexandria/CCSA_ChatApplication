using System.ComponentModel.DataAnnotations;

namespace CCSA_ChatApp.Domain.DTOs.UserDTOs
{
    public class PasswordDTO
    {
        [Required(ErrorMessage = "Enter Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [Password, StringLength(7, ErrorMessage = "Password must be more than 4 and less than 7 characters", MinimumLength = 4)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Password and ConfirmPassword do not match")]
        public string ConfirmPassword { get; set; }
    }
}
