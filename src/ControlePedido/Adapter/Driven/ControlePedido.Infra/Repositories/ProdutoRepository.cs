using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControlePedido.Infra.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ControlePedidoContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public ProdutoRepository(ControlePedidoContext context)
    {
        _context = context;
    }

    public void Criar(Produto produto)
    {
        _context.Add(produto);
    }

    public Task<List<Produto>> ListarPorCategoria(Categoria categoria)
    {
        return _context.Produto.AsNoTracking().Where(x => x.Categoria == categoria).ToListAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
