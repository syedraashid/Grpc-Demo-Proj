
using Grpc.Core;
using Shared.Protos;

namespace ServiceC.Grpc.HostedServices
{
    public class ProductService:Product.ProductBase
    {
        public override Task<ProductResponse> GetProduct(ProductRequest ProductRequest, ServerCallContext context)
        {
            var Response = new ProductResponse
            {
                ProductId = "1",
                ProductName = "NewProductName",
                RemaninigCount = 12,
                CategoryDetails = new Category
                {
                    CategoryId = "1",
                    CategoryName = "CategorynName"
                }
            };
            return Task.FromResult(Response);
        }

        public override Task<ProductListResponse> GetAllProducts(EmptyRequest ProductRequest, ServerCallContext context)
        {
            var Response = new ProductListResponse();
            
                Response.Products.AddRange(new[]
                {
                    new ProductResponse()
                        {
                            ProductId = "1",
                            ProductName = "NewProductName",
                            RemaninigCount = 12,
                            CategoryDetails = new Category
                            {
                                CategoryId = "1",
                                CategoryName = "CategorynName"
                            }
                        },
                        new ProductResponse()
                        {
                            ProductId = "1",
                            ProductName = "NewProductName",
                            RemaninigCount = 12,
                            CategoryDetails = new Category
                            {
                                CategoryId = "1",
                                CategoryName = "CategorynName"
                            }
                        }
                });

            return Task.FromResult(Response);
        }
    }
}
