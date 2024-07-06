using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Domain.Adapters.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
	{
		Guid Criar(Cliente cliente);
        Task<Cliente?> ConsultarPorEmail(string email);
        Task<Cliente?> ConsultarPorCpf(string cpf);
        Task<ICollection<Cliente>> ListarTodos();
    }
}

