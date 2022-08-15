using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain
{
    public class UsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            /*  //Get dependency injection of user
              string username = value.ToString();
              //Get existing user
              *//*User currentUser = _user.GetUser(username).Result;*//*
              if (currentUser == null)
              {
                  return ValidationResult.Success;
              }*/
            return new ValidationResult("Username already exist");
        }
    }
}
