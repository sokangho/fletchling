using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fletchling.Data.Models
{
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
