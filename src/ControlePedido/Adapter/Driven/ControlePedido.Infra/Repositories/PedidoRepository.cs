using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            _context.Entry(pedido).State = EntityState.Added;

            foreach (var item in pedido.Itens)
                _context.Entry(item).State = EntityState.Added;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

