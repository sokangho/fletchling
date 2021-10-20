using Fletchling.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fletchling.Business.Contracts
{
    public interface ITimelineService
    {
        Task<TimelineGroup> GetTimelinesByGroupAsync(string uid, string timelineGroupName);
        Task SetTimelinesInGroupAsync(string uid, List<string> timelines, string groupName = "All");
    }
}
