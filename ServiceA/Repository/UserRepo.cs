using Google.Cloud.Firestore;
using Service.Shared;

namespace ServiceA.Repository
{
    public class UserRepo : BaseRepository,IUserRepo
    {
        private const string UsersCollection = "users";
        public UserRepo(FirestoreDb db) : base(db)
        {
        }

        public async Task<UserModel?> GetUser(string userEamil)
        {
            DocumentReference docRef = _db.Collection(UsersCollection).Document(userEamil);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<UserModel>();
            }
            return null;
        }

        public async Task<WriteResult> AddUser(UserModel user)
        {
            DocumentReference docRef = _db.Collection(UsersCollection).Document(user.Email);

            return await docRef.SetAsync(user);
        }

    }
    
    public interface IUserRepo
    {
        Task<UserModel?> GetUser(string userEamil);
        Task<WriteResult> AddUser(UserModel user);
    }

}