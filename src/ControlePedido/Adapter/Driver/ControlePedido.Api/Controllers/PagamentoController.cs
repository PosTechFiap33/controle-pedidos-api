using ControlePedido.Api.Base;
using ControlePedido.Application.DTOs;
using ControlePedido.Application.UseCases.Pedidos;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedido.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController : MainController
	{
		public PagamentoController()
		{
		}

        [HttpPost(Name = "PostPagamento")]
        public async Task<IActionResult> Post([FromServices] IPagarPedidoUseCase useCase, [FromBody] PagarPedidoDTO pagarPedido)
        {
            await useCase.Executar(pagarPedido);
            return CustomResponse();
        }
    }
}

