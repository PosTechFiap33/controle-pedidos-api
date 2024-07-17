using System.Net;
using System.Text.Json.Nodes;
using ControlePedido.Api.Base;
using ControlePedido.Application.DTOs;
using ControlePedido.Application.UseCases.Pedidos;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedido.Api.Controllers
{
    /// <summary>
    /// Controlador para recebimento de pagamentos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController : MainController
	{
        /// <summary>
        /// Realiza o pagamento de um pedido.
        /// </summary>
        /// <param name="pagamento">Dados do pagamento realizado.</param>
        /// <returns>Pagamento efetivado.</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(500, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> Post([FromServices] IPagarPedidoUseCase useCase, [FromBody] PagarPedidoDTO pagarPedido)
        {
            await useCase.Executar(pagarPedido);
            return CustomResponse(statusCode: HttpStatusCode.Created);
        }
    }
}

