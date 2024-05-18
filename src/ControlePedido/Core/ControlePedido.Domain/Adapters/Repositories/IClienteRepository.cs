using System;
using System.Threading.Tasks;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Domain.Adapters.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
	{
		Guid Criar(Cliente cliente);
        Task<bool> ConsultarPorEmail(string email);
        Task<bool> ConsultarPorCpf(string cpf);
        Task<Cliente?> ConsultarPorId(Guid id);
    }
}

