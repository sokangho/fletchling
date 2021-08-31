using Fletchling.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fletchling.Data.Repositories
{
    public interface ITimelineRepository
    {
        Task<TimelineGroup> GetTimelineGroupByNameAsync(string uid, string timelineGroupName);
        Task SetTimelinesInGroupAsync(string uid, List<string> timelines, string groupName = "All");
    }
}
