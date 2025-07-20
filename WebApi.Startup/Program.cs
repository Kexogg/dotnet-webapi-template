using Scalar.AspNetCore;
using WebApi.Infrastructure;
using WebApi.Startup.Extensions;
using WebApi.Startup.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog()
    .AddAppSettings()
    .AddCors()
    .AddJwtAuth()
    .AddControllersWithErrorHandling();

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>()
    .AddOpenApi()
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

await app.MigrateDbAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

await app.RunAsync();