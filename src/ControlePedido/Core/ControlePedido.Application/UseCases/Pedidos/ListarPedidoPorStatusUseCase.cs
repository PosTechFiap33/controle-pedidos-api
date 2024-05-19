using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Application.UseCases.Pedidos
{
    public interface IListarPedidoPorStatusUseCase
	{
		Task<ICollection<Pedido>> Executar(StatusPedido? status);
	}

	public class ListarPedidoPorStatusUseCase : IListarPedidoPorStatusUseCase
	{
		private readonly IPedidoRepository _repository;

        public ListarPedidoPorStatusUseCase(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Pedido>> Executar(StatusPedido? status)
        {
            return await _repository.ListarPorStatus(status);
        }
    }
}

