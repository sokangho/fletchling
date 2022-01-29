using System.Collections.Generic;
using System.Threading.Tasks;
using Fletchling.Domain.Entities;

namespace Fletchling.Application.Interfaces.Services
{
    public interface ITimelineService
    {
        Task<TimelineGroup> GetTimelineGroupByNameAsync(string uid, string timelineGroupName);
        Task SetTimelinesInGroupAsync(string uid, List<string> timelines, string groupName = "All");
    }
}