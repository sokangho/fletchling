using Fletchling.Data.Models;
using Google.Cloud.Firestore;
using System.Threading.Tasks;

namespace Fletchling.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FirestoreDb _firestoreDb;

        public UserRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }
        
        public async Task AddUser(TwitterUserCredentials credentials)
        {
            CollectionReference collection = _firestoreDb.Collection("users");
            DocumentReference docRef = collection.Document(credentials.UserId.ToString());

            await docRef.SetAsync(credentials);
        }
    }
}
