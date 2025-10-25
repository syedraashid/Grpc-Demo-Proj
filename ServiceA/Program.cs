using ServiceA.Grpc.clientServices;
using ServiceA.Grpc.HostedServices;
using Shared.Protos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<Product.ProductClient>(o =>
{
    o.Address = new Uri("https://localhost:7000");
});

builder.Services.AddGrpcClient<Order.OrderClient>(o =>
{
    o.Address = new Uri("https://localhost:6000");
});

builder.Services.AddScoped<IProductClientService, ProductclientServices>();
builder.Services.AddScoped<IOrderClientService, OrderclientServices>();

builder.Services.AddControllers();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapControllers();

app.MapGrpcService<UserService>();

app.MapGet("/", () => "Service A is Running!");

app.Run();
