using ControlePedido.Application.DTOs;

namespace ControlePedido.Application.UseCases.Pedidos;

public interface IPagarPedidoManualmenteUseCase
{
    Task Executar(PagarPedidoManualDTO pagarPedido);
}
