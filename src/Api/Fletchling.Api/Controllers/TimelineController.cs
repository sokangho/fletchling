using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fletchling.Application.Exceptions;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Domain.ApiModels.Requests;
using Fletchling.Domain.Entities;

namespace Fletchling.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimelineController : ControllerBase
    {
        private readonly IAuthorizationService _authService;
        private readonly ITimelineService _timelineService;

        public TimelineController(IAuthorizationService authService, ITimelineService timelineService)
        {
            _authService = authService;
            _timelineService = timelineService;
        }

        [HttpGet]
        public async Task<ActionResult<TimelineGroup>> GetTimelinesByGroup([FromQuery, Required] string timelineGroupName)
        {
            var uid = User.Claims.Where(x => x.Type == "user_id").Select(x => x.Value).FirstOrDefault();

            var result = await _timelineService.GetTimelinesByGroupAsync(uid, timelineGroupName);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<ActionResult> SetTimelinesInGroup([FromBody] SetTimelineRequest request)
        {
            var authResult = await _authService.AuthorizeAsync(User, request, "OwnerPolicy");

            if (!authResult.Succeeded)
            {
                throw new BusinessException(HttpStatusCode.Forbidden, "No permission to set timeline.");
            }

            try
            {
                await _timelineService.SetTimelinesInGroupAsync(request.UID, request.Timelines, request.GroupName);
            }
            catch (DataNotFoundException ex)
            {
                // throw BusinessException so that it gets handled by GlobalExceptionMiddleware
                throw new BusinessException(HttpStatusCode.NotFound, ex.Message); 
            }
            
            return Ok();
        }
    }
}
