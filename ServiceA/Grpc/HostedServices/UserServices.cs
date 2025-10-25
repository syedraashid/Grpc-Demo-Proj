
using Grpc.Core;
using Service.Shared;
using ServiceA.Repository;

namespace ServiceA.Grpc.HostedServices
{
    public class UserService:User.UserBase
    {
        protected readonly IUserRepo _repo;

        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }
        public override async Task<UserResponse> GetUser(UserRequest userRequest, ServerCallContext context)
        {
            var userData = await _repo.GetUser(userRequest.Name);
            var Response = new UserResponse();

            if (userData != null)
            {
                Response.Name = userData.UserName;
                Response.Email = userData.Email;
                Response.DateOfBirth = userData.DateOfBirth;
                
            }
            return Response;
        }
    }
}
