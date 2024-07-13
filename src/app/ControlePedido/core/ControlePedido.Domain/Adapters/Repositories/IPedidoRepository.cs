using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Domain.Adapters.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
	{
        void Criar(Pedido pedido);
        void Atualizar(Pedido pedido);
        Task<Pedido?> ConsultarPorId(Guid pedidoId);
        Task<ICollection<Pedido>> ListarPorStatus(StatusPedido? status);
    }
}

