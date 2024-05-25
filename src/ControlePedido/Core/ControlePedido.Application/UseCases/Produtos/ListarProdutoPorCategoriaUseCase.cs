using ControlePedido.Application.DTOs;
using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Application.UseCases.Produtos;

public interface IListarProdutoPorCategoriaUseCase
{
    Task<ICollection<ProdutoDTO>> Executar(Categoria categoria);
}

public class ListarProdutoPorCategoriaUseCase : IListarProdutoPorCategoriaUseCase
{
    private readonly IProdutoRepository _repository;

    public ListarProdutoPorCategoriaUseCase(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<ProdutoDTO>> Executar(Categoria categoria)
    {
        if (categoria <= 0 || categoria == null)
            throw new DomainException("Categoria inválida!");

        var produtos = await _repository.ListarPorCategoria(categoria);

        return produtos.Select(p => new ProdutoDTO(p))
                       .ToList();
    }
}
