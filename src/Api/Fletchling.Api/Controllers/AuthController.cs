using Fletchling.Api.Constants;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Domain.ApiModels.Requests;
using Fletchling.Domain.ApiModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fletchling.Api.Controllers
{
    [Route("api")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [Route(RouteConstants.AuthRoutes.CREATE_JWT)]
        [HttpPost]
        public ActionResult<CreateJwtResponse> CreateJwt(CreateJwtRequest request)
        {
            var token = _jwtService.CreateJwt(request);
            return Ok(new CreateJwtResponse { Jwt = token });
        }
    }
}