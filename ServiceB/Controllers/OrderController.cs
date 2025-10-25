

using Microsoft.AspNetCore.Mvc;
using Service.Shared;
using ServiceA.Repository;
using ServiceB.Grpc.clientServices;

namespace ServiceB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IProductClientService _ProductClientService;
        private readonly IUserClientService _UserClientService;
        private readonly IOrderRepo _repo;

        public OrderController(IProductClientService ProductClientService,IUserClientService UserClientService,IOrderRepo orderRepo)
        {
            _ProductClientService = ProductClientService;
            _UserClientService = UserClientService;
            _repo = orderRepo;
        }

        [HttpGet("{GetUserDetails}")]
        public async Task<IActionResult> GetUserDetails(string name)
        {
            var response = await _UserClientService.GetUsersAsync(name);
            return Ok(response);
        }

        [HttpPost("{AddOrder}")]
        public async Task<IActionResult> AddNewUser(OrderModel order)
        {
            await _repo.AddOrder(order);
            return Ok("Created");
        }
    }
}

