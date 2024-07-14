using ControlePedido.Application.DTOs;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Application.UseCases.Pedidos
{
    public interface IListarPedidoUseCase
    {
        Task<ICollection<PedidoDTO>> Executar(StatusPedido? status);
    }
}

