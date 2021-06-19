using Fletchling.Api.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Fletchling.Api.Authorization
{
    public class IsOwnerAuthorizationHandler : AuthorizationHandler<IsOwnerRequirement, TimelineRequest>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOwnerRequirement requirement, TimelineRequest request)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }
           
            var uid = context.User.Claims.Where(x => x.Type == "user_id").Select(x => x.Value).FirstOrDefault();

            if (uid == request.UID)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
