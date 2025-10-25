
namespace ServiceB.Grpc.clientServices
{
    public class BaseGrpcClient<TClient>
    {
        protected readonly TClient _client;

        public BaseGrpcClient(TClient client)
        {
            _client = client;
        }
    }
}