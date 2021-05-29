using Fletchling.Twitter.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fletchling.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        private readonly TwitterSearchUser _twitterSearchUser;

        public TwitterController(TwitterSearchUser twitterSearchUser)
        {
            _twitterSearchUser = twitterSearchUser;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _twitterSearchUser.GetUsersByNameAsync();
            return Ok(res);
        }
    }
}
