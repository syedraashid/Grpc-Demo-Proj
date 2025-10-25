using Service.Shared;

namespace ServiceA.Grpc.clientServices
{
    public interface IProductClientService
    {
        Task<ProductResponse> GetProductsAsync(string productId);
        Task<ProductListResponse> GetAllProductsAsync();
    }

    public class ProductclientServices: BaseGrpcClient<Product.ProductClient>,IProductClientService
    {
        public ProductclientServices(Product.ProductClient client) : base(client)
        {
        }

        public async Task<ProductListResponse> GetAllProductsAsync()
        {
            var Response = await _client.GetAllProductsAsync(new EmptyRequest());

            return Response;
        }

        public async Task<ProductResponse> GetProductsAsync(string productId)
        {
            var Response = await _client.GetProductAsync(new ProductRequest
            {
                ProductId = productId
            });

            return Response;
        }

    }
}