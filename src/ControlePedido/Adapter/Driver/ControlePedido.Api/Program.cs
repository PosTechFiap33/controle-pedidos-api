using ControlePedido.Api.Configuration;
using ControlePedido.Api.Middleware;
using ControlePedido.Infra.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerApp();

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    services.ConfigureMigrationDatabase();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

