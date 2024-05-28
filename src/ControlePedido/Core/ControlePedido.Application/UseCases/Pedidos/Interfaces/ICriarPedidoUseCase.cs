using ControlePedido.Application.DTOs;

namespace ControlePedido.Application.UseCases.Pedidos
{
    public interface ICriarPedidoUseCase
    {
        Task<PedidoCriadoDTO> Executar(CriarPedidoDTO criarPedidoDTO);
    }
}

