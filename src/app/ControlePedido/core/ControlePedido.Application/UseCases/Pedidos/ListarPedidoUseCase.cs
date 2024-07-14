using ControlePedido.Application.DTOs;
using ControlePedido.CrossCutting;
using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Application.UseCases.Pedidos
{

    public class ListarPedidoUseCase : IListarPedidoUseCase
    {
        private readonly IPedidoRepository _repository;

        public ListarPedidoUseCase(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<PedidoDTO>> Executar(StatusPedido? status)
        {
            var pedidos = await _repository.ListarPedidos(status);
            return pedidos
                    .Select(pedido => new PedidoDTO(pedido))
                    .OrderByDescending(p => p.Status == StatusPedido.PRONTO.GetDescription())
                    .ThenByDescending(p => p.Status == StatusPedido.EM_PREPARACAO.GetDescription())
                    .ThenByDescending(p => p.Status == StatusPedido.RECEBIDO.GetDescription())
                    .ToList();
        }
    }
}

