using Fletchling.Twitter.Models;
using Fletchling.Twitter.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Fletchling.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterController : ControllerBase
    {        
        private readonly ITwitterService _twitterService;

        public TwitterController(ITwitterService twitterService)
        {            
            _twitterService = twitterService;
        }

        [Route("user/get")]
        [HttpGet]
        public async Task<ActionResult<TwitterUser>> GetTwitterUser([FromQuery, Required] long twitterUserId)
        {
            var user = await _twitterService.GetUserAsync(twitterUserId);
            return user;
        }

        [Route("user/search")]
        [HttpGet]
        public async Task<ActionResult<List<TwitterUser>>> SearchTwitterUser([FromQuery, Required] string username)
        {
            var users = await _twitterService.SearchUsersAsync(username);            
            return users;
        }
    }
}
