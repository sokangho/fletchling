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
        
        public async Task AddUserAsync(User user)
        {
            CollectionReference colRef = _firestoreDb.Collection(FirestoreConstants.UsersCollection);
            DocumentReference docRef = colRef.Document(user.UID.ToString());

            await docRef.SetAsync(user);
        }

        public async Task<User> GetUserAsync(string uid)
        {
            DocumentReference docRef = _firestoreDb.Collection(FirestoreConstants.UsersCollection).Document(uid);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<User>();
            }
            return null;
        }
    }
}
