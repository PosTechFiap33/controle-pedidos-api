using ControlePedido.Api.Configuration;
using ControlePedido.Api.Middleware;
using ControlePedido.Infra.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.AddHealthChecks()
       .AddCheck("self", () => HealthCheckResult.Healthy())
       .AddNpgSql(
           connectionString: builder.Configuration["DbConnection"],
           healthQuery: "SELECT 1;",
           name: "postgres",
           failureStatus: HealthStatus.Degraded);

var app = builder.Build();

app.UseSwaggerApp();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
services.ConfigureMigrationDatabase();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health");
});

app.Run();

