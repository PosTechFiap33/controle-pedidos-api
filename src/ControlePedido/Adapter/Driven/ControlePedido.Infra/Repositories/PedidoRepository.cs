using System.Net.NetworkInformation;
using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

            foreach (var status in pedido.Status)
                _context.Entry(status).State = EntityState.Added;
        }

        //TODO - avaliar maneiras melhores de fazer
        public void Atualizar(Pedido pedido)
        {
            if (pedido.Pagamento.PedidoId == Guid.Empty)
                _context.Entry(pedido.Pagamento).State = EntityState.Added;
            else
                _context.Entry(pedido.Pagamento).State = EntityState.Modified;

            foreach(var status in pedido.Status)
            {
                if(status.PedidoId == Guid.Empty)
                    _context.Entry(status).State = EntityState.Added;
                else
                    _context.Entry(status).State = EntityState.Modified;
            }

            _context.Pedido.Attach(pedido);
        }

        public Task<Pedido?> ConsultarPorId(Guid pedidoId)
        {
            return _context.Pedido
                           .Include(p => p.Status)
                           .Include(p => p.Pagamento)
                           .AsNoTracking()
                           .FirstOrDefaultAsync(p => p.Id == pedidoId);
        }

        public async Task<ICollection<Pedido>> ListarPorStatus(StatusPedido? status)
        {
            IQueryable<Pedido> query = _context.Pedido
                                               .Include(p => p.Status)
                                               .AsNoTracking();

            if (status.HasValue)
            {
                query = query.Where(p => p.Status
                             .OrderByDescending(s => s.DataHora)
                             .FirstOrDefault().Status == status);
            }

            return await query.ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

