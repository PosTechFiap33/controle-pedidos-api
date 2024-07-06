using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlePedido.Infra.Repositories
{
    public class ClienteRepository : IClienteRepository
	{
        private readonly ControlePedidoContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ClienteRepository(ControlePedidoContext context)
        {
            _context = context;
        }

        public Guid Criar(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            return cliente.Id;
        }

        public Task<Cliente?> ConsultarPorEmail(string email)
        {
            return _context.Cliente.AsNoTracking().FirstOrDefaultAsync(c => c.Email.Endereco == email);
        }

        public Task<Cliente?> ConsultarPorCpf(string cpf)
        {
            return _context.Cliente.AsNoTracking().FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
        }

        public async Task<ICollection<Cliente>> ListarTodos()
        {
            return await _context.Cliente.AsNoTracking().ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

