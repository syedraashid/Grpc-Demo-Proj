using ServiceB.Grpc.clientServices;
using ServiceB.Grpc.HostedServices;
using Service.Shared;
using ServiceA.Repository;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    var http1Port = int.Parse(builder.Configuration["ASPNETCORE_URLS"]?.Split(';')[0].Split(':').Last() ?? "80");
    var http2Port = http1Port + 1; // Assign the gRPC port one number higher (e.g., 7001)

    serverOptions.ListenAnyIP(http1Port, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1; // Only accepts HTTP/1.1
    });

    serverOptions.ListenAnyIP(http2Port, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2; // Only accepts HTTP/2
    });
});

var productServiceUrl = builder.Configuration["GrpcServices:ProductService"] ?? "";
var userServiceUrl = builder.Configuration["GrpcServices:UserService"] ?? "";

builder.Services.AddGrpcClient<Product.ProductClient>(o =>
{
    o.Address = new Uri(productServiceUrl);
});

builder.Services.AddGrpcClient<User.UserClient>(o =>
{
    o.Address = new Uri(userServiceUrl);
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

app.MapGet("/", () => "Service B is Running! Man");

app.Run();