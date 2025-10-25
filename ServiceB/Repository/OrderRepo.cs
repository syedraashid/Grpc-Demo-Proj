using Google.Cloud.Firestore;
using Service.Shared;

namespace ServiceA.Repository
{
    public class OrderRepo : BaseRepository,IOrderRepo
    {
        private const string OrdersCollection = "Orders";
        public OrderRepo(FirestoreDb db) : base(db)
        {
        }

        public async Task<OrderModel?> GetOrder(string OrderId)
        {
            DocumentReference docRef = _db.Collection(OrdersCollection).Document(OrderId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<OrderModel>();
            }
            return null;
        }
        public async Task<List<OrderModel>> GetAllOrders()
        {
            CollectionReference docRef = _db.Collection(OrdersCollection);
            QuerySnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Documents.Any())
            {
                return snapshot.Documents
                .Where(doc => doc.Exists) 
                .Select(doc => doc.ConvertTo<OrderModel>()) 
                .ToList();
            }

            return null;
        }

        public async Task<WriteResult> AddOrder(OrderModel Order)
        {
            DocumentReference docRef = _db.Collection(OrdersCollection).Document(Order.OrderId);

            return await docRef.SetAsync(Order);
        }

    }
    
    public interface IOrderRepo
    {
        Task<OrderModel?> GetOrder(string email);
        Task<List<OrderModel>> GetAllOrders();
        Task<WriteResult> AddOrder(OrderModel Order);
    }

}