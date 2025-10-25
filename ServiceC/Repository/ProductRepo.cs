using Google.Cloud.Firestore;
using Service.Shared;

namespace ServiceA.Repository
{
    public class ProductRepo : BaseRepository,IProductRepo
    {
        private const string ProductsCollection = "Products";
        public ProductRepo(FirestoreDb db) : base(db)
        {
        }

        public async Task<ProductModel?> GetProduct(string ProductId)
        {
            DocumentReference docRef = _db.Collection(ProductsCollection).Document(ProductId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<ProductModel>();
            }
            return null;
        }

        public async Task<List<ProductModel>> GetAllProducts()
        {
            CollectionReference docRef = _db.Collection(ProductsCollection);
            QuerySnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Documents.Any())
            {
                return snapshot.Documents
                .Where(doc => doc.Exists) 
                .Select(doc => doc.ConvertTo<ProductModel>()) 
                .ToList();
            }

            return null;
        }

        public async Task<WriteResult> AddProduct(ProductModel Product)
        {
            DocumentReference docRef = _db.Collection(ProductsCollection).Document(Product.ProductId);

            return await docRef.SetAsync(Product);
        }

    }
    
    public interface IProductRepo
    {
        Task<ProductModel?> GetProduct(string ProductId);
        Task<List<ProductModel>> GetAllProducts();
        Task<WriteResult> AddProduct(ProductModel Product);
    }

}