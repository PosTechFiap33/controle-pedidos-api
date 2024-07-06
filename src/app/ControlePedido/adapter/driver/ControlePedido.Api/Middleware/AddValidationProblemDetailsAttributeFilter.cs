using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace ControlePedido.Api.Middleware
{
    public class AddValidationProblemDetailsAttributeFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Não faz nada antes da execução da ação
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Adiciona o atributo [ProducesResponseType] com status code 400 e tipo ValidationProblemDetails
            var objectResult = context.Result as ObjectResult;

            if (objectResult?.StatusCode >= 400)
            {
                var actionDescriptor = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
                var methodInfo = actionDescriptor?.MethodInfo;

                var producesResponseTypeAttributes = methodInfo?.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), true)
                    .Concat(methodInfo.DeclaringType.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), true))
                    .OfType<ProducesResponseTypeAttribute>();

                if (!producesResponseTypeAttributes.Any(a => a.StatusCode == 400 && a.Type == typeof(ValidationProblemDetails)))
                {
                    objectResult.DeclaredType = typeof(ValidationProblemDetails);
                    objectResult.StatusCode = 400;
                }
            }
        }
    }
}
