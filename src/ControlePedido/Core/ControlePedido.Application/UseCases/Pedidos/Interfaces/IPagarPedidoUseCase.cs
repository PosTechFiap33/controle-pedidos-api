using ControlePedido.Application.DTOs;

namespace ControlePedido.Application.UseCases.Pedidos
{
    public interface IPagarPedidoUseCase
	{
		Task Executar(PagarPedidoDTO pagarPedido);
	}
}

