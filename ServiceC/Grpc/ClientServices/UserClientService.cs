using Service.Shared;

namespace ServiceC.Grpc.clientServices
{
    public interface IUserClientService
    {
        Task<UserResponse> GetUsersAsync(string Name);
    }

    public class UserclientServices: BaseGrpcClient<User.UserClient>,IUserClientService
    {
        public UserclientServices(User.UserClient client) : base(client)
        {
        }


        public async Task<UserResponse> GetUsersAsync(string Name)
        {
            var Response = await _client.GetUserAsync(new UserRequest
            {
                Name = Name
            });

            return Response;
        }

    }
}