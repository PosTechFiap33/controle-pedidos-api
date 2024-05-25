using ControlePedido.Application.DTOs;
using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.ValueObjects;

namespace ControlePedido.Application.UseCases.Produtos;

public interface ICriarProdutoUseCase
{
    Task<Guid> Executar(CriarProdutoDTO criarProdutoDto);
}

public class CriarProdutoUseCase : ICriarProdutoUseCase
{
    private readonly IProdutoRepository _repository;

    public CriarProdutoUseCase(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Executar(CriarProdutoDTO criarProdutoDto)
    {
        var imagem = new Imagem(criarProdutoDto.UrlImagem, criarProdutoDto.ExtensaoImagem, criarProdutoDto.Nome);
        
        var produto = new Produto(criarProdutoDto.Nome, criarProdutoDto.Preco, criarProdutoDto.Categoria, criarProdutoDto.Descricao, imagem);

        _repository.Criar(produto);
       
        await _repository.UnitOfWork.Commit();

        return produto.Id;
    }
}
