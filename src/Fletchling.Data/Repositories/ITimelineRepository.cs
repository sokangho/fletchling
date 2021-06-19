using Fletchling.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fletchling.Data.Repositories
{
    public interface ITimelineRepository
    {
        Task<TimelineGroup> GetTimelineGroupByNameAsync(string uid, string timelineGroupName);
        Task AddTimelineToGroupAsync(string uid, string twitterUsername, string groupName = "All");
        Task RemoveTimelineFromGroupAsync(string uid, string twitterUsername, string groupName = "All");
        Task SetTimelinesInGroupAsync(string uid, List<string> timelines, string groupName = "All");
    }
}
