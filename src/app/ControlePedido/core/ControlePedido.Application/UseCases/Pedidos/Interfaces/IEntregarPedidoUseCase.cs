namespace ControlePedido.Application.UseCases.Pedidos
{
    public interface IEntregarPedidoUseCase
	{
		Task Executar(Guid pedidoId);
	}
}

