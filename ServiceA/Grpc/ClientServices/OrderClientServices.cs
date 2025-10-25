using Service.Shared;

namespace ServiceA.Grpc.clientServices
{
    public interface IOrderClientService
    {
        Task<OrderResponse> GetOrdersAsync(string Useremail);
        Task<OrderListResponse> GetAllOrdersAsync();
    }

    public class OrderclientServices: BaseGrpcClient<Order.OrderClient>,IOrderClientService
    {
        public OrderclientServices(Order.OrderClient client) : base(client)
        {
        }

        public async Task<OrderListResponse> GetAllOrdersAsync()
        {
            var Response = await _client.GetAllOrdersAsync(new EmptyRequest());

            return Response;
        }

        public async Task<OrderResponse> GetOrdersAsync(string Orderid)
        {
            var Response = await _client.GetOrderAsync(new OrderRequest
            {
                OrderId = Orderid
            });

            return Response;
        }

    }
}