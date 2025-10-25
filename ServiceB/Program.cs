using ServiceB.Grpc.clientServices;
using ServiceB.Grpc.HostedServices;
using Service.Shared;
using ServiceA.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<Product.ProductClient>(o =>
{
    o.Address = new Uri("https://localhost:7000");
});

builder.Services.AddGrpcClient<User.UserClient>(o =>
{
    o.Address = new Uri("https://localhost:5000");
});
builder.Services.AddFirestore(builder.Configuration);
builder.Services.AddScoped<IUserClientService, UserclientServices>();
builder.Services.AddScoped<IProductClientService, ProductclientServices>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.MapGrpcService<OrderService>();

app.MapGet("/", () => "Service B is Running!");

app.Run();