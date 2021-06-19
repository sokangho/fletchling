using Google.Cloud.Firestore;
using System.Collections.Generic;

namespace Fletchling.Data.Models
{
    [FirestoreData]
    public class TimelineCollection
    {
        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public List<string> Timelines { get; set; }
    }
}
