using Fletchling.Api.Exceptions;
using Fletchling.Api.Models;
using Fletchling.Data.Exceptions;
using Fletchling.Data.Models;
using Fletchling.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
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

        [HttpPatch]
        public async Task<ActionResult> SetTimelinesInGroup([FromBody] TimelineRequest request)
        {
            var authResult = await _authService.AuthorizeAsync(User, request, "OwnerPolicy");

            if (!authResult.Succeeded)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "No permission to set timeline.");
            }

            try
            {
                await _timelineRepo.SetTimelinesInGroupAsync(request.UID, request.Timelines, request.GroupName);
            }
            catch (DataNotFoundException ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ex.Message); 
            }
            
            return Ok();
        }
    }
}
