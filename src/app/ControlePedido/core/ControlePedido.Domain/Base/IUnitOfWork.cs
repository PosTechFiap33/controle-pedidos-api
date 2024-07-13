using System.Threading.Tasks;

namespace ControlePedido.Domain.Base
{
    public interface IUnitOfWork
	{
        Task<bool> Commit();
    }
}

