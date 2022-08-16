using Microsoft.AspNetCore.Authorization;


namespace CCSA_ChatApp.Authentication.Services
{
    public class AdminRequirement: IAuthorizationRequirement
    {
        public AdminRequirement(string role)
        {
            Role = role;
        }
        public string Role;
    }
}
