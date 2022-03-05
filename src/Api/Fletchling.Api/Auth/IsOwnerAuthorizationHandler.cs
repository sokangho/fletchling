using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Fletchling.Api.Auth
{
    public class IsOwnerAuthorizationHandler : AuthorizationHandler<IsOwnerRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            IsOwnerRequirement requirement, string ownerUserId)
        {
            if (context.User == null || context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var uid = context.User.Claims.Where(x => x.Type == "TwitterUserId")
                             .Select(x => x.Value)
                             .FirstOrDefault();

            if (uid == ownerUserId)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
    
    public class IsOwnerRequirement : IAuthorizationRequirement { }
}