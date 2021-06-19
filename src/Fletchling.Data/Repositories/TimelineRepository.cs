using Fletchling.Data.Models;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fletchling.Data.Repositories
{
    public class TimelineRepository : ITimelineRepository
    {
        private readonly FirestoreDb _firestoreDb;

        public TimelineRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task<TimelineGroup> GetTimelineGroupByNameAsync(string uid, string timelineGroupName)
        {
            CollectionReference timelineGroupCollectionRef = _firestoreDb
                                                            .Collection(FirestoreConstants.UsersCollection)
                                                            .Document(uid)
                                                            .Collection(FirestoreConstants.TimelineGroupsCollection);

            Query getTimelineGroupByNameQuery = timelineGroupCollectionRef.WhereEqualTo(FirestoreConstants.TimelineGroupName, timelineGroupName).Limit(1);
            QuerySnapshot snapshot = await getTimelineGroupByNameQuery.GetSnapshotAsync();

            if (snapshot.Documents.Any())
            {
                return snapshot.Documents[0].ConvertTo<TimelineGroup>();
            }

            // TODO: Handle exception better
            throw new KeyNotFoundException("Collection Name does not exist");
        }

        public async Task AddTimelineToGroupAsync(string uid, string twitterUsername, string groupName = "All")
        {
            var timelineGroup = await GetTimelineGroupByNameAsync(uid, groupName);
            var timelineGroupRef = timelineGroup.Reference;

            await timelineGroupRef.UpdateAsync(FirestoreConstants.TimelineGroupTimelines, FieldValue.ArrayUnion(twitterUsername));                                       
        }

        public async Task RemoveTimelineFromGroupAsync(string uid, string twitterUsername, string groupName = "All")
        {
            var timelineGroup = await GetTimelineGroupByNameAsync(uid, groupName);
            var timelineGroupRef = timelineGroup.Reference;

            await timelineGroupRef.UpdateAsync(FirestoreConstants.TimelineGroupTimelines, FieldValue.ArrayRemove(twitterUsername));  
        }

        public async Task SetTimelinesInGroupAsync(string uid, List<string> timelines, string groupName = "All")
        {
            var timelineGroup = await GetTimelineGroupByNameAsync(uid, groupName);
            var timelineGroupRef = timelineGroup.Reference;

            await timelineGroupRef.UpdateAsync(FirestoreConstants.TimelineGroupTimelines, timelines);
        }
    }
}
