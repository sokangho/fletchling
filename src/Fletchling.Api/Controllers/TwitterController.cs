using Fletchling.Twitter.Models;
using Fletchling.Twitter.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetinvi.Models;

namespace Fletchling.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        private readonly TwitterSearchUser _twitterSearchUser;
        private readonly ITwitterService _twitterService;

        public TwitterController(TwitterSearchUser twitterSearchUser, ITwitterService twitterService)
        {
            _twitterSearchUser = twitterSearchUser;
            _twitterService = twitterService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get(string username)
        {
            var res = await _twitterService.SearchUsersAsync(username);
            return res;
        }
    }
}
