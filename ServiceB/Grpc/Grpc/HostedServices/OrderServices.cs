
using Grpc.Core;
using Shared.Protos;

namespace ServiceB.Grpc.HostedServices
{
    public class OrderService:Order.OrderBase
    {
        public override Task<OrderResponse> GetOrder(OrderRequest OrderRequest, ServerCallContext context)
        {
            var Response = new OrderResponse
            {
                OrderId = "1",
                OrderAmount = 655,
                OrderStatus = "Pending",
                PaymentDetails = new Payment
                {
                    PaymentId = "1",
                    PaymentStatus = "Done"
                }
            };
            return Task.FromResult(Response);
        }

        public override Task<OrderListResponse> GetAllOrders(EmptyRequest OrderRequest, ServerCallContext context)
        {
            var Response = new OrderListResponse();
            
                Response.Orders.AddRange(new[]
                {
                    new OrderResponse()
                        {
                            OrderId = "1",
                            OrderAmount = 655,
                            OrderStatus = "Pending",
                            PaymentDetails = new Payment
                            {
                                PaymentId = "1",
                                PaymentStatus = "Done"
                            }
                        },
                        new OrderResponse()
                        {
                            OrderId = "1",
                            OrderAmount = 655,
                            OrderStatus = "Pending",
                            PaymentDetails = new Payment
                            {
                                PaymentId = "1",
                                PaymentStatus = "Done"
                            }
                        }
                });

            return Task.FromResult(Response);
        }
    }
}
