using ControlePedido.Application.DTOs;

namespace ControlePedido.Application.UseCases.Clientes
{
    public interface IListarTodosClientesUseCase
	{
		Task<ICollection<ClienteDTO>> Executar();
	}
}

