using Google.Cloud.Firestore;

namespace Service.Shared;

public class BaseRepository
{
    protected readonly FirestoreDb _db;
    public BaseRepository(FirestoreDb db)
    {
        _db = db;
    }
}