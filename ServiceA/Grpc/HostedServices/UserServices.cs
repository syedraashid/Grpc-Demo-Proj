
using Grpc.Core;
using Shared.Protos;

namespace ServiceA.Grpc.HostedServices
{
    public class UserService:User.UserBase
    {
        public override Task<UserResponse> GetUser(UserRequest userRequest, ServerCallContext context)
        {
            var Response = new UserResponse
            {
                Name = "User",
                Email = "Email",    
                DateOfBirth = "DOB"
            };
            return Task.FromResult(Response);
        }
    }
}
