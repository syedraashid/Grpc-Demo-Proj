using ServiceA.Grpc.clientServices;
using ServiceA.Grpc.HostedServices;
using Service.Shared;
using ServiceA.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<Product.ProductClient>(o =>
{
    o.Address = new Uri("https://localhost:7000");
});

builder.Services.AddGrpcClient<Order.OrderClient>(o =>
{
    o.Address = new Uri("https://localhost:6000");
});
builder.Services.AddFirestore(builder.Configuration);
builder.Services.AddScoped<IProductClientService, ProductclientServices>();
builder.Services.AddScoped<IOrderClientService, OrderclientServices>();
builder.Services.AddScoped<IUserRepo,UserRepo>();

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

app.MapGrpcService<UserService>();

app.MapGet("/", () => "Service A is Running!");

app.Run();
