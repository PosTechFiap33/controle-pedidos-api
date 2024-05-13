using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Application.UseCases.Clientes
{
    public interface ICriarClienteUseCase
	{
		Task<Cliente> Executar(string nome, string cpf, string email);
	}

    public class CriarClienteUseCase : ICriarClienteUseCase
    {
        private readonly IClienteRepository _repository;

        public CriarClienteUseCase(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Cliente> Executar(string nome, string cpf, string email)
        {
            var cliente = new Cliente(nome, cpf, email);

            var cpfExiste = await _repository.consultarPorCpf(cpf);

            if (cpfExiste)
                throw new DomainException("Cpf já cadastrado no sistema!");

            var emailExiste = await _repository.consultarPorEmail(email);

            if (emailExiste)
                throw new DomainException("E-mail já cadastrado no sistema!");

            _repository.Criar(cliente);

            await _repository.UnitOfWork.Commit();

            return cliente;
        }
    }
}

