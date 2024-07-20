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

            foreach (var status in pedido.Status)
            {
                if (status.PedidoId == Guid.Empty)
                    _context.Entry(status).State = EntityState.Added;
                else
                    _context.Entry(status).State = EntityState.Modified;
            }

            _context.Pedido.Attach(pedido);
        }

        public Task<Pedido?> ConsultarPorId(Guid pedidoId)
        {
            return _context.Pedido
                           .Include(p => p.Cliente)
                           .Include(p => p.Status)
                           .Include(p => p.Pagamento)
                           .FirstOrDefaultAsync(p => p.Id == pedidoId);
        }

        public async Task<ICollection<Pedido>> ListarPedidos(StatusPedido? status)
        {
            var statusDesejados = new List<StatusPedido> {
                StatusPedido.PRONTO,
                StatusPedido.EM_PREPARACAO,
                StatusPedido.RECEBIDO
             };

            IQueryable<Pedido> query = _context.Pedido
                                         .AsNoTracking()
                                         .Include(p => p.Itens)
                                         .ThenInclude(i => i.Produto)
                                         .Include(p => p.Cliente)
                                         .Include(p => p.Status);

            if (status.HasValue)
            {
                query = query.Where(p => p.Status
                             .OrderByDescending(s => s.DataHora)
                             .FirstOrDefault().Status == status);
            }
            else
            {
                query = query.Where(p =>
                    statusDesejados.Contains(p.Status
                        .OrderByDescending(s => s.DataHora)
                        .FirstOrDefault().Status));

                query = query.OrderBy(p =>
                        p.Status.OrderByDescending(s => s.DataHora)
                                .FirstOrDefault()
                                .DataHora);
            }

            return await query.ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

