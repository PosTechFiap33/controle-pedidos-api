using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Infra.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ControlePedidoContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public PedidoRepository(ControlePedidoContext context)
        {
            _context = context;
        }

        public void Criar(Pedido pedido)
        {
            _context.Pedido.Add(pedido);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

