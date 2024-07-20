using System.Net;
using ControlePedido.Api.Base;
using ControlePedido.Application.DTOs;
using ControlePedido.Application.UseCases.Pedidos;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedido.Api;

/// <summary>
/// Controlador para o webhook de integracao com o mercado pago.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MercadoPagoController : MainController
{
    public MercadoPagoController(ILogger<MercadoPagoController> logger) : base(logger)
    {
    }

    /// <summary>
    /// Processa um pagamento utilizando o Mercado Pago.
    /// </summary>
    /// <remarks>
    /// Recebe os dados de pagamento e executa o caso de uso para realizar o pagamento.
    /// </remarks>
    /// <param name="pagamento">Os dados de pagamento fornecidos pelo cliente.</param>
    /// <param name="useCase">A instância do caso de uso para processar o pagamento.</param>
    /// <returns>Uma resposta customizada com o status da operação.</returns>
    [HttpPost()]
    public async Task<IActionResult> Pagamento([FromBody] PagamentoMercadoPagoDTO pagamento, [FromServices] IPagarPedidoUseCase useCase)
    {
        await useCase.Executar(new PagarPedidoDTO(pagamento.Dados.TransacaoId, pagamento.Dados.Valor));

        return CustomResponse(null, HttpStatusCode.Created);
    }
}
