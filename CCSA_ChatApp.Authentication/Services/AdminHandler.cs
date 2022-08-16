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
            var groupName = authContext.Request.Query.Where(s => s.Key == "groupName").Select(s=>s.Value).FirstOrDefault();
            if (groupRole.Contains(groupName))
            {
              context.Succeed(requirement);
            }
        }
    }
}
