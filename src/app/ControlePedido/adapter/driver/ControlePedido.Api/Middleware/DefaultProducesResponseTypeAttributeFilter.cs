using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControlePedido.Api.Middleware
{
    public class DefaultProducesResponseTypeAttributeFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.StatusCode >= 400)
            {
                objectResult.DeclaredType = typeof(ValidationProblemDetails);
                objectResult.StatusCode = 400;
            }
        }
    }
}

