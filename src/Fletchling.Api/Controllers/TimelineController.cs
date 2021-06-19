using Fletchling.Api.Models;
using Fletchling.Data.Models;
using Fletchling.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fletchling.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimelineController : ControllerBase
    {
        private readonly IAuthorizationService _authService;
        private readonly ITimelineRepository _timelineRepo;

        public TimelineController(IAuthorizationService authService, ITimelineRepository timelineRepo)
        {
            _authService = authService;
            _timelineRepo = timelineRepo;
        }

        [HttpGet]
        public async Task<ActionResult<TimelineGroup>> GetTimelinesByGroup([FromQuery, Required] string timelineGroupName)
        {
            var uid = User.Claims.Where(x => x.Type == "user_id").Select(x => x.Value).FirstOrDefault();

            var result = await _timelineRepo.GetTimelineGroupByNameAsync(uid, timelineGroupName);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddTimelineToGroup([FromBody] TimelineRequest request)
        {
            var authResult = await _authService.AuthorizeAsync(User, request, "OwnerPolicy");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            try
            {
                await _timelineRepo.AddTimelineToGroupAsync(request.UID, request.TwitterUsername, request.GroupName);
            }
            catch (Exception ex)
            {
                // TODO: Handle exception better
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveTimelineFromGroup([FromBody] TimelineRequest request)
        {
            var authResult = await _authService.AuthorizeAsync(User, request, "OwnerPolicy");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            try
            {
                await _timelineRepo.RemoveTimelineFromGroupAsync(request.UID, request.TwitterUsername, request.GroupName);
            }
            catch (Exception ex)
            {
                // TODO: Handle exception better
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> SetTimelinesInGroup([FromBody] TimelineRequest request)
        {
            var authResult = await _authService.AuthorizeAsync(User, request, "OwnerPolicy");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            try
            {
                await _timelineRepo.SetTimelinesInGroupAsync(request.UID, request.Timelines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
    }
}
