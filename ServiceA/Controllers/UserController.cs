

using Microsoft.AspNetCore.Mvc;
using ServiceA.Grpc.clientServices;

namespace ServiceA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IProductClientService _ProductClientService;
        private readonly IOrderClientService _OrderClientService;

        public UserController(IProductClientService ProductClientService,IOrderClientService OrderClientService)
        {
            _ProductClientService = ProductClientService;
            _OrderClientService = OrderClientService;
        }

        [HttpGet("{GetUserOrder}")]
        public async Task<IActionResult> GetUserOrderDetails(string Email)
        {
            var response = await _OrderClientService.GetOrdersAsync(Email);
            return Ok(response);
        }
    }
}

