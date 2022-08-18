using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain.DTOs.GroupChatDTOs
{
    public class GroupChatCreateDTO
    {
        [Required]
        public string GroupName { get; set; }

        [Required]
        public string GroupDescription { get; set; }

        [Required]
        public IFormFile Picture { get; set; }
    }
}
