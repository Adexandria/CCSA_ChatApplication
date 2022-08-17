using CCSA_ChatApp.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace CCSA_ChatApp.Domain
{
    public class UsernameAttribute : ValidationAttribute
    {
        /*protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _userService = validationContext.GetService(typeof(IUserService)) as IUserService;
            string username = value.ToString();
            //Get existing user
            User currentUser = _userService.GetUser(username).Result;
            if (currentUser == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Username already exist");
        }*/
    }
}
