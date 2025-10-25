using ServiceC.Grpc.clientServices;
using ServiceC.Grpc.HostedServices;
using Shared.Protos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<Order.OrderClient>(o =>
{
    o.Address = new Uri("https://localhost:6000");
});

builder.Services.AddGrpcClient<User.UserClient>(o =>
{
    o.Address = new Uri("https://localhost:5000");
});

builder.Services.AddScoped<IUserClientService, UserclientServices>();
builder.Services.AddScoped<IOrderClientService, OrderclientServices>();

builder.Services.AddControllers();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapControllers();

app.MapGrpcService<ProductService>();

app.MapGet("/", () => "Service C is Running!");

app.Run();