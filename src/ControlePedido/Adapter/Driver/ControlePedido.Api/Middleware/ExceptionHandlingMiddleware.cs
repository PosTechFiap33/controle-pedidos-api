using System.Net;
using System.Text.Json;
using ControlePedido.Domain.Base;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedido.Api.Middleware
{

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode status = HttpStatusCode.InternalServerError)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var errorDetails = new ValidationProblemDetails(new Dictionary<string, string[]> {
                {
                    "Mensagens", new string[]{exception.Message}
                }
            });

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));
        }

    }
}

