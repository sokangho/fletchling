﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Fletchling.Application.Exceptions;
using Fletchling.Application.Interfaces.Repositories;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Domain.Entities;

namespace Fletchling.Application.Services
{
    public class TimelineService : ITimelineService
    {
        private readonly ITimelineRepository _timelineRepo;

        public TimelineService(ITimelineRepository timelineRepo)
        {
            _timelineRepo = timelineRepo;
        }

        public async Task<TimelineGroup> GetTimelinesByGroupAsync(string uid, string timelineGroupName)
        {
            var timelines = await _timelineRepo.GetTimelineGroupByNameAsync(uid, timelineGroupName);

            if (timelines == null)
                throw new DataNotFoundException($"Timeline group with name: '{timelineGroupName}' for uid: '{uid}' does not exist.");

            return timelines;
        }

        public async Task SetTimelinesInGroupAsync(string uid, List<string> timelines, string groupName = "All")
        {
            await _timelineRepo.SetTimelinesInGroupAsync(uid, timelines, groupName);
        }
    }
}