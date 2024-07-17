using System.Net;
using ControlePedido.Api.Base;
using ControlePedido.Application.DTOs;
using ControlePedido.Application.UseCases.Pedidos;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedido.Api;

[ApiController]
[Route("api/[controller]")]
public class MercadoPagoController : MainController
{

    [HttpPost()]
    public async Task<IActionResult> Pagamento([FromBody] PagamentoMercadoPagoDTO pagamento, [FromServices] IPagarPedidoUseCase useCase)
    {
        await useCase.Executar(new PagarPedidoDTO(pagamento.Dados.TransacaoId));

        return CustomResponse(null, HttpStatusCode.Created);
    }
}
