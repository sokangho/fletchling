using System.Collections.Generic;
using System.Text.Json.Serialization;
using Google.Cloud.Firestore;

namespace Fletchling.Domain.Entities
{
    // TODO: Refactor this class to avoid dependencies on firebase 
    [FirestoreData]
    public class TimelineGroup
    {
        [FirestoreDocumentId, JsonIgnore]
        public DocumentReference Reference { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public List<string> Timelines { get; set; }
    }
}