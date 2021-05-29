using Google.Cloud.Firestore;
using System.Threading.Tasks;

namespace Fletchling.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly FirestoreDb _firestoreDb;

        public ClientRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task AddClient()
        {
            CollectionReference collection = _firestoreDb.Collection("clients");
            await collection.AddAsync(new
            {
                Name = "test"
            });
        }
    }
}
