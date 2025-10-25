

using Microsoft.AspNetCore.Mvc;
using ServiceC.Grpc.clientServices;

namespace ServiceC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IOrderClientService _OrderClientService;
        private readonly IUserClientService _UserClientService;

        public ProductController(IOrderClientService OrderClientService,IUserClientService UserClientService)
        {
            _OrderClientService = OrderClientService;
            _UserClientService = UserClientService;
        }

        [HttpGet("{GetAllOrders}")]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _OrderClientService.GetAllOrdersAsync();
            return Ok(response);
        }
    }
}

