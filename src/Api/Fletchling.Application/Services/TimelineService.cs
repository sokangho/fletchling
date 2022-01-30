using System.Collections.Generic;
using System.Threading.Tasks;
using Fletchling.Application.Exceptions;
using Fletchling.Application.Interfaces.Repositories;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Fletchling.Application.Services
{
    public class TimelineService : ITimelineService
    {
        private readonly ITimelineRepository _timelineRepo;
        private readonly ILogger<TimelineService> _logger;

        public TimelineService(ITimelineRepository timelineRepo, ILogger<TimelineService> logger)
        {
            _timelineRepo = timelineRepo;
            _logger = logger;
        }

        public async Task<TimelineGroup> GetTimelineGroupByNameAsync(string uid, string timelineGroupName)
        {
            var timelineGroup = await _timelineRepo.GetTimelineGroupByNameAsync(uid, timelineGroupName);
            return timelineGroup;
        }

        public async Task SetTimelinesInGroupAsync(string uid, List<string> timelines, string groupName = "All")
        {
            var timelineGroup = await _timelineRepo.GetTimelineGroupByNameAsync(uid, groupName);

            if (timelineGroup == null)
            {
                _logger.LogWarning($"Timeline group with name: '{groupName}' for uid: '{uid}' does not exist.");
                throw new DataNotFoundException(
                    $"Timeline group with name: '{groupName}' does not exist.");
            }

            await _timelineRepo.SetTimelinesInGroupAsync(timelineGroup, timelines);
        }
    }
}