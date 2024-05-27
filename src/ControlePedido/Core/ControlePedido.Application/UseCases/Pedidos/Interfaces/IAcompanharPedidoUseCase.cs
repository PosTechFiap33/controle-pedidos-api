using ControlePedido.Application.DTOs;

namespace ControlePedido.Application.UseCases.Pedidos;

public interface IAcompanharPedidoUseCase
{
    Task<AcompanhamentoPedidoDTO> Executar(Guid pedidoId);
}
