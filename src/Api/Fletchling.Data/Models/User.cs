using Google.Cloud.Firestore;

namespace Fletchling.Data.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreDocumentId]
        public string UID { get; set; }

        [FirestoreProperty]
        public long TwitterUserId { get; set; }

        [FirestoreProperty]
        public string AccessToken { get; set; }

        [FirestoreProperty]
        public string AccessTokenSecret { get; set; }        
    }
}
