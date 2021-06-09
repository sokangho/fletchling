﻿using Google.Cloud.Firestore;

namespace Fletchling.Data.Models
{
    [FirestoreData]
    public class TwitterUserCredentials
    {
        [FirestoreProperty]
        public string UserId { get; set; }

        [FirestoreProperty]
        public string AccessToken { get; set; }

        [FirestoreProperty]
        public string AccessTokenSecret { get; set; }
    }
}
