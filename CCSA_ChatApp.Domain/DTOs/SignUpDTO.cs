using CCSA_ChatApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain.DTOs
{
    public class SignUpDTO
    {
        public Country Country { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }
        public IFormFile Picture { get; set; }
    }
}
