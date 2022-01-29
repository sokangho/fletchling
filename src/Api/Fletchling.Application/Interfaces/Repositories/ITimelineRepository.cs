using System.Collections.Generic;
using System.Threading.Tasks;
using Fletchling.Domain.Entities;
using Google.Cloud.Firestore;

namespace Fletchling.Application.Interfaces.Repositories
{
    public interface ITimelineRepository
    {
        Task<TimelineGroup> GetTimelineGroupByNameAsync(string uid, string timelineGroupName);
        Task SetTimelinesInGroupAsync(TimelineGroup timelineGroup, List<string> timelines);
    }
}