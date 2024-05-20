using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Domain.Adapters.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        void Criar(Produto produto);
        Task<Produto?> ConsultarPorId(Guid produtoId);
        Task<ICollection<Produto>> ListarPorCategoria(Categoria categoria);       
    }
}
