namespace ControlePedido.Application.UseCases.Pedidos
{
    public interface IFinalizarPreparoPedidoUseCase
    {
        Task Executar(Guid pedidoId);
    }
}

