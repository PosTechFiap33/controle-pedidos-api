﻿using ControlePedido.Domain.Adapters.Repositories;
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

        public Task<bool> ConsultarPorEmail(string email)
        {
            return _context.Cliente.AsNoTracking().AnyAsync(c => c.Email.Endereco == email);
        }

        public Task<bool> ConsultarPorCpf(string cpf)
        {
            return _context.Cliente.AsNoTracking().AnyAsync(c => c.Cpf.Numero == cpf);
        }

        public Task<Cliente?> ConsultarPorId(Guid id)
        {
            return _context.Cliente.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

