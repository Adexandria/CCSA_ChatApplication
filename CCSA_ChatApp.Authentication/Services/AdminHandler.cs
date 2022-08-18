using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CCSA_ChatApp.Authentication.Services
{
    public class AdminHandler : AuthorizationHandler<AdminRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            var groupRole = context.User.Claims.Where(s=>s.Type==ClaimTypes.Role && s.Value.Contains(requirement.Role)).Select(s=>s.Value).ToList();
            if(groupRole is null)
            {
                return;
            }
            var authContext = context.Resource as HttpContext;
            var groupPath = authContext.Request.Path.Value;
            var groupName = groupPath.Split('/');
            var validName= groupRole.FirstOrDefault(s => s.Contains(groupName[3]));
            if (validName is not null)
            {
              context.Succeed(requirement);
            }
        }
    }
}
