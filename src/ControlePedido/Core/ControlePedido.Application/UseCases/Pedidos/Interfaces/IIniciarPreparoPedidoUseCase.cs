namespace ControlePedido.Application.UseCases.Pedidos
{
    public interface IIniciarPreparoPedidoUseCase
	{
		Task Executar(Guid pedidoId);
	}
}

