using System.Collections.Generic;
using System.Threading.Tasks;
using Fletchling.Application.Exceptions;
using Fletchling.Application.Interfaces.Repositories;
using Fletchling.Domain.ApiModels.Requests;
using Fletchling.Domain.Entities;
using Google.Cloud.Firestore;

namespace Fletchling.Data.FirebaseRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FirestoreDb _firestoreDb;

        public UserRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }
        
        public async Task AddUserAsync(AddUserRequest user)
        {
            // Create new User document
            CollectionReference usersColRef = _firestoreDb.Collection(FirestoreConstants.UsersCollection);
            DocumentReference userDocRef = usersColRef.Document(user.UID.ToString());
            await userDocRef.SetAsync(user);

            // Initialise TimelineGroups Collection inside the newly created User document
            CollectionReference timelineGroupsColRef = _firestoreDb
                                                       .Collection(FirestoreConstants.UsersCollection)
                                                       .Document(userDocRef.Id)
                                                       .Collection(FirestoreConstants.TimelineGroupsCollection);
            var timelineGroup = new TimelineGroup
            {
                Name = "All",
                Timelines = new List<string>()
            };
            await timelineGroupsColRef.AddAsync(timelineGroup);                        
        }

        public async Task<User> GetUserAsync(string uid)
        {
            DocumentReference docRef = _firestoreDb.Collection(FirestoreConstants.UsersCollection).Document(uid);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<User>();
            }
            
            throw new DataNotFoundException($"User with uid: {uid} not found.");
        }
    }
}