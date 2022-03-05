using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Domain.ApiModels;
using Microsoft.AspNetCore.Authorization;

namespace Fletchling.Api.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class TwitterController : ControllerBase
    {        
        private readonly ITwitterService _twitterService;

        public TwitterController(ITwitterService twitterService)
        {            
            _twitterService = twitterService;
        }

        [Route(RouteConstants.TwitterRoutes.GetUser)]
        [HttpGet]
        public async Task<ActionResult<TwitterUser>> GetTwitterUser([FromQuery, Required] long twitterUserId)
        {
            var user = await _twitterService.GetUserAsync(twitterUserId);
            return Ok(user);
        }

        [Route(RouteConstants.TwitterRoutes.SearchUsers)]
        [HttpGet]
        public async Task<ActionResult<List<TwitterUser>>> SearchTwitterUser([FromQuery, Required] string username)
        {
            var users = await _twitterService.SearchUsersAsync(username);            
            return Ok(users);
        }
    }
}
