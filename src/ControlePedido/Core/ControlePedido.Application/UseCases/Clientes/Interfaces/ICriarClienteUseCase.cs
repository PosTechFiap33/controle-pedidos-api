namespace ControlePedido.Application.UseCases.Clientes
{
    public interface ICriarClienteUseCase
	{
		Task<Guid> Executar(string nome, string cpf, string email);
	}
}

