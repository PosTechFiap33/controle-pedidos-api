using System;
using System.Threading.Tasks;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Domain.Adapters.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
	{
		Guid Criar(Cliente cliente);
        Task<bool> consultarPorEmail(string email);
        Task<bool> consultarPorCpf(string cpf);
    }
}

