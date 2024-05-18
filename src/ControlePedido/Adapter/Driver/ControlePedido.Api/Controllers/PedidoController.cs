using ControlePedido.Api.Base;
using ControlePedido.Application.DTOs;
using ControlePedido.Application.UseCases.Pedidos;
using ControlePedido.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedido.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : MainController
    {
        [HttpPost(Name = "PostPedido")]
        public async Task<ActionResult<Produto>> Post([FromServices] ICriarPedidoUseCase useCase, [FromBody] CriarPedidoDTO criarPedido)
        {
            var result = await useCase.Executar(criarPedido);
            return CustomResponse(result);
        }
    }
}

