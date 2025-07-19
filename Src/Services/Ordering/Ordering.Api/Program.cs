using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();
    

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    app.InitializeDatabaseAsync();
}

app.Run();