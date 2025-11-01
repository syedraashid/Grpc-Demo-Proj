

using Microsoft.AspNetCore.Mvc;
using Service.Shared;
using ServiceA.Repository;
using ServiceC.Grpc.clientServices;

namespace ServiceC.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IOrderClientService _OrderClientService;
        private readonly IUserClientService _UserClientService;
        private readonly IProductRepo _repo;

        public ProductController(IOrderClientService OrderClientService,IUserClientService UserClientService,IProductRepo repo)
        {
            _OrderClientService = OrderClientService;
            _UserClientService = UserClientService;
            _repo = repo;
        }

        [HttpGet(Name = "GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _OrderClientService.GetAllOrdersAsync();
            return Ok(response);
        }

        [HttpPost(Name = "AddNewProduct")]
        public async Task<IActionResult> AddNewProduct(ProductModel product)
        {
            await _repo.AddProduct(product);
            return Ok("Created");
        }
    }
}