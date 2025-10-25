using ServiceC.Grpc.clientServices;
using ServiceC.Grpc.HostedServices;
using Service.Shared;
using ServiceA.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<Order.OrderClient>(o =>
{
    o.Address = new Uri("https://localhost:6000");
});

builder.Services.AddGrpcClient<User.UserClient>(o =>
{
    o.Address = new Uri("https://localhost:5000");
});
builder.Services.AddFirestore(builder.Configuration);
builder.Services.AddScoped<IUserClientService, UserclientServices>();
builder.Services.AddScoped<IOrderClientService, OrderclientServices>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();

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

app.MapGrpcService<ProductService>();

app.MapGet("/", () => "Service C is Running!");

app.Run();