using Discount.Grpc.Data;
using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddDbContext<DiscountContext>(optins =>
{
    optins.UseSqlite(builder.Configuration.GetConnectionString("DbConnection"));
});

var app = builder.Build();

app.UseMigration();

app.MapGrpcService<DiscountService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();