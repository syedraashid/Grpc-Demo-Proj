
using Grpc.Core;
using Service.Shared;
using ServiceA.Repository;

namespace ServiceB.Grpc.HostedServices
{
    public class OrderService:Order.OrderBase
    {
        protected readonly IOrderRepo _repo;

        public OrderService(IOrderRepo repo)
        {
            _repo = repo;
        }

        public override async Task<OrderResponse> GetOrder(OrderRequest OrderRequest, ServerCallContext context)
        {
            var Data = await _repo.GetOrder(OrderRequest.OrderId);
            var Response = new OrderResponse();

            if (Data != null)
            {
                Response.OrderId = Data.OrderId;
                Response.OrderAmount = Data.OrderAmount;
                Response.OrderStatus = Data.OrderStatus;
                Response.PaymentDetails = new Payment
                {
                    PaymentId = Data.PaymentDetails.PaymentId,
                    PaymentStatus = Data.PaymentDetails.PaymentStatus,
                };
            }
            
            return Response;
        }

        public override async Task<OrderListResponse> GetAllOrders(EmptyRequest OrderRequest, ServerCallContext context)
        {
            var Data = await _repo.GetAllOrders();
            var Response = new OrderListResponse();

            var OrderResponses = Data.Select(_ => new OrderResponse
            {
                OrderId = _.OrderId,
                OrderAmount = _.OrderAmount,
                OrderStatus = _.OrderStatus,
                PaymentDetails = new Payment
                {
                    PaymentId = _.PaymentDetails.PaymentId,
                    PaymentStatus = _.PaymentDetails.PaymentStatus
                }
            }).ToList();

            Response.Orders.AddRange(OrderResponses);
            return Response;
        }
    }
}
