using ControlePedido.Application.DTOs;
using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Application.UseCases.Pedidos
{
    public interface IListarPedidoPorStatusUseCase
    {
        Task<ICollection<PedidoDTO>> Executar(StatusPedido? status);
    }

    public class ListarPedidoPorStatusUseCase : IListarPedidoPorStatusUseCase
    {
        private readonly IPedidoRepository _repository;

        public ListarPedidoPorStatusUseCase(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<PedidoDTO>> Executar(StatusPedido? status)
        {
            var pedidos = await _repository.ListarPorStatus(status);

            return pedidos.Select(pedido => new PedidoDTO(pedido)).ToList();
        }
    }
}

