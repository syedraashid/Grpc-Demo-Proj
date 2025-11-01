

using Microsoft.AspNetCore.Mvc;
using Service.Shared;
using ServiceA.Grpc.clientServices;
using ServiceA.Repository;

namespace ServiceA.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IProductClientService _ProductClientService;
        private readonly IOrderClientService _OrderClientService;
        private readonly IUserRepo _repo;

        public UserController(IProductClientService ProductClientService,IOrderClientService OrderClientService,IUserRepo repo)
        {
            _ProductClientService = ProductClientService;
            _OrderClientService = OrderClientService;
            _repo = repo;
        }

        [HttpGet(Name = "GetUserOrderDetails")]
        public async Task<IActionResult> GetUserOrderDetails(string orderId)
        {
            var response = await _OrderClientService.GetOrdersAsync(orderId);
            return Ok(response);
        }

        [HttpPost(Name = "AddNewUser")]
        public async Task<IActionResult> AddNewUser(UserModel user)
        {
            await _repo.AddUser(user);
            return Ok("Created");
        }
    }
}