using BuildingBlocks.Behavior;
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("DbConnection")!);
    config.CreateDatabasesForTenants(c => { });
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();