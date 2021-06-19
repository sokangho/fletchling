using System.Threading.Tasks;

namespace Fletchling.Data.Repositories
{
    public interface ITimelineRepository
    {
        Task AddTimelineToGroup(string uid, string twitterUsername, string groupName = "All");
        Task RemoveTimelineFromGroup(string uid, string twitterUsername, string groupName = "All");
    }
}
