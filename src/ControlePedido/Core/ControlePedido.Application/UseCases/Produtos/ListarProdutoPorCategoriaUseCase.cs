using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Application.UseCases.Produtos;

public interface IListarProdutoPorCategoriaUseCase
{
    Task<ICollection<Produto>> Executar(Categoria categoria);
}

public class ListarProdutoPorCategoriaUseCase : IListarProdutoPorCategoriaUseCase
{
    private readonly IProdutoRepository _repository;

    public ListarProdutoPorCategoriaUseCase(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public Task<ICollection<Produto>> Executar(Categoria categoria)
    {
        if (categoria <= 0 || categoria == null)
            throw new DomainException("Categoria inválida!");

        return _repository.ListarPorCategoria(categoria);
    }
}
