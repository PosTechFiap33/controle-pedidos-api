using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Domain.Adapters.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
	{
		void Criar(Pedido pedido);
	}
}

