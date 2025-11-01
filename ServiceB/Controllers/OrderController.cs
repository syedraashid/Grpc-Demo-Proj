

using Microsoft.AspNetCore.Mvc;
using Service.Shared;
using ServiceA.Repository;
using ServiceB.Grpc.clientServices;

namespace ServiceB.Controllers
{
    [ApiController]
    [Route("api/[action]")]
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

        [HttpGet(Name = "GetUserDetails")]
        public async Task<IActionResult> GetUserDetails(string email)
        {
            var response = await _UserClientService.GetUsersAsync(email);
            return Ok(response);
        }

        [HttpPost(Name = "AddNewUser")]
        public async Task<IActionResult> AddNewUser(OrderModel order)
        {
            await _repo.AddOrder(order);
            return Ok("Created");
        }
    }
}

