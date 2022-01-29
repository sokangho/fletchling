using Microsoft.AspNetCore.Authorization;

namespace Fletchling.Api.Authorization
{
    public class IsOwnerRequirement : IAuthorizationRequirement
    {
        public IsOwnerRequirement()
        { }
    }
}