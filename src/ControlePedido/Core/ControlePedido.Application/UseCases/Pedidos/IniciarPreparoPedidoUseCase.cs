using ControlePedido.Domain.Adapters.Providers;
using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Base;

namespace ControlePedido.Application.UseCases.Pedidos
{
    public interface IIniciarPreparoPedidoUseCase
	{
		Task Executar(Guid pedidoId);
	}

    public class IniciarPreparoPedidoUseCase : IIniciarPreparoPedidoUseCase
	{
		private readonly IPedidoRepository _repository;

        public IniciarPreparoPedidoUseCase(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task Executar(Guid pedidoId)
        {
            var pedido = await _repository.ConsultarPorId(pedidoId);

            if (pedido is null)
                throw new DomainException("Não foi encontrado um pedido para o código informado!");

            pedido.IniciarPreparo();

            _repository.Atualizar(pedido);

            await _repository.UnitOfWork.Commit();
        }
    }
}

