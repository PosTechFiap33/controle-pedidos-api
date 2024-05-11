namespace ControlePedido.Infra
{
	public interface IUnitOfWork
	{
        Task<bool> Commit();
    }
}

