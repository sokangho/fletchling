using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Fletchling.Api.Constants;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Domain.ApiModels;
using Fletchling.Domain.ApiModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fletchling.Api.Controllers
{
    [Route("api")]
    [Authorize]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        private readonly ITwitterService _twitterService;

        public TwitterController(ITwitterService twitterService)
        {
            _twitterService = twitterService;
        }

        [Route(RouteConstants.TwitterRoutes.GET_USER)]
        [HttpGet]
        public async Task<ActionResult<TwitterUser>> GetTwitterUser([FromQuery, Required] long twitterUserId)
        {
            var user = await _twitterService.GetUserAsync(twitterUserId);
            return Ok(user);
        }

        [Route(RouteConstants.TwitterRoutes.SEARCH_USER)]
        [HttpGet]
        public async Task<ActionResult<SearchTwitterUserResponse>> SearchTwitterUser([FromQuery, Required] string username)
        {
            var users = await _twitterService.SearchUsersAsync(username);
            return Ok(new SearchTwitterUserResponse { Users = users });
        }
    }
}