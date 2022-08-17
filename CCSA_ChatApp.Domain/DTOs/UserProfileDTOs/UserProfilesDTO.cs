using CCSA_ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Domain.DTOs.UserProfileDTOs
{
    public class UserProfilesDTO
    {
        public  Country Country { get; set; }
        public  string Username { get; set; }
        public  List<GroupChat> GroupChats { get; set; }
    }
}
