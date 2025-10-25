

using Microsoft.AspNetCore.Mvc;
using ServiceB.Grpc.clientServices;

namespace ServiceB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IProductClientService _ProductClientService;
        private readonly IUserClientService _UserClientService;

        public OrderController(IProductClientService ProductClientService,IUserClientService UserClientService)
        {
            _ProductClientService = ProductClientService;
            _UserClientService = UserClientService;
        }

        [HttpGet("{GetUserDetails}")]
        public async Task<IActionResult> GetUserDetails(string name)
        {
            var response = await _UserClientService.GetUsersAsync(name);
            return Ok(response);
        }
    }
}

