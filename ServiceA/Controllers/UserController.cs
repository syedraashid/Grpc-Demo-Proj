

using Microsoft.AspNetCore.Mvc;
using Service.Shared;
using ServiceA.Grpc.clientServices;
using ServiceA.Repository;

namespace ServiceA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("{GetUserOrder}")]
        public async Task<IActionResult> GetUserOrderDetails(string Email)
        {
            var response = await _OrderClientService.GetOrdersAsync(Email);
            return Ok(response);
        }

        [HttpPost("{AddUser}")]
        public async Task<IActionResult> AddNewUser(UserModel user)
        {
            await _repo.AddUser(user);
            return Ok("Created");
        }
    }
}

