using Fletchling.Business;
using Fletchling.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fletchling.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(User user)
        {
            await _userService.AddUserAsync(user);
            return Ok();
        }
    }
}
