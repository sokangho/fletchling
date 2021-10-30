using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Domain.ApiModels.Requests;

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
        public async Task<ActionResult> AddUser(AddUserRequest user)
        {
            await _userService.AddUserAsync(user);
            return Ok();
        }
    }
}
