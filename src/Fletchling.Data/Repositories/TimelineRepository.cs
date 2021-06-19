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

        public async Task AddTimelineToGroup(string uid, string twitterUsername, string groupName = "All")
        {
            var timelineGroup = await GetTimelineGroupByName(uid, groupName);
            var timelineGroupRef = timelineGroup.Reference;

            await timelineGroupRef.UpdateAsync(FirestoreConstants.TimelineGroupTimelines, FieldValue.ArrayUnion(twitterUsername));                
                       
        }

        public async Task RemoveTimelineFromGroup(string uid, string twitterUsername, string groupName = "All")
        {
            var timelineGroup = await GetTimelineGroupByName(uid, groupName);
            var timelineGroupRef = timelineGroup.Reference;

            await timelineGroupRef.UpdateAsync(FirestoreConstants.TimelineGroupTimelines, FieldValue.ArrayRemove(twitterUsername));
  
        }

        private async Task<DocumentSnapshot> GetTimelineGroupByName(string uid, string collectionName)
        {
            CollectionReference timelineGroupCollectionRef = _firestoreDb
                                                            .Collection(FirestoreConstants.UsersCollection)
                                                            .Document(uid)
                                                            .Collection(FirestoreConstants.TimelineGroupsCollection);

            Query getTimelineGroupByNameQuery = timelineGroupCollectionRef.WhereEqualTo(FirestoreConstants.TimelineGroupName, collectionName).Limit(1);
            QuerySnapshot snapshot = await getTimelineGroupByNameQuery.GetSnapshotAsync();

            if (snapshot.Documents.Any())
            {
                return snapshot.Documents[0];
            }

            // TODO: Handle exception better
            throw new KeyNotFoundException("Collection Name does not exist");
        }
    }
}
