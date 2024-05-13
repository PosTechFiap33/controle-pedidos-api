using System.Net;
using ControlePedido.Api.Configuration;
using ControlePedido.Domain.Base;
using ControlePedido.Infra.Configuration;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApiConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    services.ConfigureMigrationDatabase();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "text/plain";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature != null)
        {
            var exceptionType = exceptionHandlerPathFeature.Error.GetType();

            if (exceptionType == typeof(DomainException))
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            Console.WriteLine($"Erro: {exceptionHandlerPathFeature.Error}");
            await context.Response.WriteAsync($"Ocorreu um erro: {exceptionHandlerPathFeature.Error.Message}").ConfigureAwait(false);
        }
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

