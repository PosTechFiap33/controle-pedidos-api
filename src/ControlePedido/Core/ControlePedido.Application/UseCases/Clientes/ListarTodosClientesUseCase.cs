using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Application.UseCases.Clientes
{

    public interface IListarTodosClientesUseCase
	{
		Task<ICollection<Cliente>> Executar();
	}

	public class ListarTodosClientesUseCase : IListarTodosClientesUseCase
	{
		private readonly IClienteRepository _repository;

        public ListarTodosClientesUseCase(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Cliente>> Executar()
        {
            return await _repository.ListarTodos();
        }
    }
}

