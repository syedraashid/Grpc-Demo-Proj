using ServiceB.Grpc.clientServices;
using ServiceB.Grpc.HostedServices;
using Shared.Protos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<Product.ProductClient>(o =>
{
    o.Address = new Uri("https://localhost:7000");
});

builder.Services.AddGrpcClient<User.UserClient>(o =>
{
    o.Address = new Uri("https://localhost:5000");
});

builder.Services.AddScoped<IUserClientService, UserclientServices>();
builder.Services.AddScoped<IProductClientService, ProductclientServices>();

builder.Services.AddControllers();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapControllers();

app.MapGrpcService<OrderService>();

app.MapGet("/", () => "Service B is Running!");

app.Run();