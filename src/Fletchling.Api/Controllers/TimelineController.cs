using Fletchling.Api.Models;
using Fletchling.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Fletchling.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimelineController : ControllerBase
    {
        private readonly ITimelineRepository _timelineRepo;

        public TimelineController(ITimelineRepository timelineRepo)
        {
            _timelineRepo = timelineRepo;
        }

        [HttpPost]
        public async Task<IActionResult> AddTimelineToGroup([FromBody] TimelineRequest request)
        {
            try
            {
                await _timelineRepo.AddTimelineToGroup(request.UID, request.TwitterUsername, request.GroupName);
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
            try
            {
                await _timelineRepo.RemoveTimelineFromGroup(request.UID, request.TwitterUsername, request.GroupName);
            }
            catch (Exception ex)
            {
                // TODO: Handle exception better
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            return Ok();
        }
    }
}
