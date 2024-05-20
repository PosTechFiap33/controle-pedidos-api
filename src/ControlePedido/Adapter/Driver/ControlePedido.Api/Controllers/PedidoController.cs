using ControlePedido.Api.Base;
using ControlePedido.Application.DTOs;
using ControlePedido.Application.UseCases.Pedidos;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedido.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : MainController
    {
        [HttpGet(Name = "GetPedido")]
        public async Task<ActionResult<ICollection<Pedido>>> Get([FromServices] IListarPedidoPorStatusUseCase useCase, [FromQuery] StatusPedido? status)
        {
            var result = await useCase.Executar(status);
            return CustomResponse(result);
        }

        [HttpPost(Name = "PostPedido")]
        public async Task<ActionResult<Guid>> Post([FromServices] ICriarPedidoUseCase useCase, [FromBody] CriarPedidoDTO criarPedido)
        {
            var result = await useCase.Executar(criarPedido);
            return CustomResponse(result);
        }

        [HttpPatch("{pedidoId}/iniciar-preparo")]
        public async Task<IActionResult> IniciarPreparo([FromRoute] Guid pedidoId, [FromServices] IIniciarPreparoPedidoUseCase useCase)
        {
            await useCase.Executar(pedidoId);
            return Ok($"Preparo do pedido {pedidoId} iniciado com sucesso.");
        }

        [HttpPatch("{pedidoId}/finalizar-preparo")]
        public async Task<IActionResult> FinalizarPreparo([FromRoute] Guid pedidoId, [FromServices] IFinalizarPreparoPedidoUseCase useCase)
        {
            await useCase.Executar(pedidoId);
            return Ok($"Preparo do pedido {pedidoId} finalizado com sucesso.");
        }

        [HttpPatch("{pedidoId}/entregar")]
        public async Task<IActionResult> Entregar([FromRoute] Guid pedidoId, [FromServices] IEntregarPedidoUseCase useCase)
        {
            await useCase.Executar(pedidoId);
            return Ok($"Entrega do pedido {pedidoId} realizada com sucesso.");
        }
    }
}

