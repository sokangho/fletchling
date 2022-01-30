using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fletchling.Application.Interfaces.Repositories;
using Fletchling.Domain.Entities;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;

namespace Fletchling.Data.FirebaseRepositories
{
    public class TimelineRepository : ITimelineRepository
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly ILogger<TimelineRepository> _logger;

        public TimelineRepository(FirestoreDb firestoreDb, ILogger<TimelineRepository> logger)
        {
            _firestoreDb = firestoreDb;
            _logger = logger;
        }

        public async Task<TimelineGroup> GetTimelineGroupByNameAsync(string uid, string timelineGroupName)
        {
            CollectionReference timelineGroupCollectionRef = _firestoreDb
                                                             .Collection(FirestoreConstants.UsersCollection)
                                                             .Document(uid)
                                                             .Collection(FirestoreConstants.TimelineGroupsCollection);

            Query getTimelineGroupByNameQuery = timelineGroupCollectionRef
                                                .WhereEqualTo(FirestoreConstants.TimelineGroupName, timelineGroupName)
                                                .Limit(1);
            QuerySnapshot snapshot = await getTimelineGroupByNameQuery.GetSnapshotAsync();

            if (!snapshot.Documents.Any())
            {
                _logger.LogWarning($"Timeline group with name: '{timelineGroupName}' for uid: '{uid}' does not exist.");
                return null;
            }

            return snapshot.Documents[0]
                           .ConvertTo<TimelineGroup>();
        }

        public async Task SetTimelinesInGroupAsync(TimelineGroup timelineGroup, List<string> timelines)
        {
            try
            {
                var timelineGroupRef = timelineGroup.Reference;
                await timelineGroupRef.UpdateAsync(FirestoreConstants.TimelineGroupTimelines, timelines);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Encounter error when setting timelines in group: {timelineGroup.Name}");
                throw;
            }
        }
    }
}