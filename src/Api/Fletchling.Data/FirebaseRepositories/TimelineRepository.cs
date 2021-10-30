using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fletchling.Application.Interfaces.Repositories;
using Fletchling.Domain.Entities;
using Google.Cloud.Firestore;

namespace Fletchling.Data.FirebaseRepositories
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

            return null;
        }

        public async Task SetTimelinesInGroupAsync(string uid, List<string> timelines, string groupName = "All")
        {
            var timelineGroup = await GetTimelineGroupByNameAsync(uid, groupName);
            var timelineGroupRef = timelineGroup.Reference;

            await timelineGroupRef.UpdateAsync(FirestoreConstants.TimelineGroupTimelines, timelines);
        }
    }
}