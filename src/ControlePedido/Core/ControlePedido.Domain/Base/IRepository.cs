using System;

namespace ControlePedido.Domain.Base
{
    public interface IRepository<T> : IDisposable where T: IAggregateRoot
	{
		IUnitOfWork UnitOfWork { get; }
	}
}