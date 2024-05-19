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
        public async Task<ActionResult<Produto>> Get([FromServices] IListarPedidoPorStatusUseCase useCase, [FromQuery] StatusPedido? status)
        {
            var result = await useCase.Executar(status);
            return CustomResponse(result);
        }

        [HttpPost(Name = "PostPedido")]
        public async Task<ActionResult<Produto>> Post([FromServices] ICriarPedidoUseCase useCase, [FromBody] CriarPedidoDTO criarPedido)
        {
            var result = await useCase.Executar(criarPedido);
            return CustomResponse(result);
        }
    }
}

