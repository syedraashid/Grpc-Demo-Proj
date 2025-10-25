
using Grpc.Core;
using Service.Shared;
using ServiceA.Repository;

namespace ServiceC.Grpc.HostedServices
{
    public class ProductService:Product.ProductBase
    {
        protected readonly IProductRepo _repo;

        public ProductService(IProductRepo repo)
        {
            _repo = repo;
        }
        public override async Task<ProductResponse> GetProduct(ProductRequest ProductRequest, ServerCallContext context)
        {
            var Data = await _repo.GetProduct(ProductRequest.ProductId);
            var Response = new ProductResponse();

            if (Data != null)
            {
                Response.ProductId = Data.ProductId ?? "";
                Response.ProductName = Data.ProductName ?? "";
                Response.RemaninigCount = Data.RemaninigCount;

                if (Data.CategoryDetails != null)
                {
                    Response.CategoryDetails = new Category
                    {
                        CategoryId = Data.CategoryDetails.CategoryId ?? "",
                        CategoryName = Data.CategoryDetails.CategoryName ?? ""
                    };
                }
                else
                {
                    Response.CategoryDetails = new Category
                    {
                        CategoryId = "",
                        CategoryName = ""
                    };
                }
            }
    
            return Response;
        }

        public override async Task<ProductListResponse> GetAllProducts(EmptyRequest ProductRequest, ServerCallContext context)
        {
            var Data = await _repo.GetAllProducts();
            var Response = new ProductListResponse();

            var ProductResponses = Data.Select(_ => new ProductResponse
            {
                ProductId = _.ProductId,
                ProductName = _.ProductName,
                RemaninigCount = _.RemaninigCount,
                CategoryDetails = new Category
                {
                    CategoryId = _.CategoryDetails.CategoryId,
                    CategoryName = _.CategoryDetails.CategoryName
                }
            }).ToList();

            Response.Products.AddRange(ProductResponses);

            return Response;

        }
    }
}
