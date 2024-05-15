using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;
using ControlePedido.Domain.ValueObjects;

namespace ControlePedido.Application.UseCases.Produtos;

public class CriarProdutoDto
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public string Descricao { get; set; }
    public Categoria Categoria { get; set; }
    public string NomeImagem { get; set; }
    public string UrlImagem { get; set; }
    public string ExtensaoImagem { get; set; }
}

public interface ICriarProdutoUseCase
{
    Task<Produto> Executar(CriarProdutoDto criarProdutoDto);
}

public class CriarProdutoUseCase : ICriarProdutoUseCase
{
    private readonly IProdutoRepository _repository;

    public CriarProdutoUseCase(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Produto> Executar(CriarProdutoDto criarProdutoDto)
    {
        var imagem = new Imagem(criarProdutoDto.UrlImagem, criarProdutoDto.ExtensaoImagem, criarProdutoDto.Nome);
        
        var produto = new Produto(criarProdutoDto.Nome, criarProdutoDto.Preco, criarProdutoDto.Categoria, criarProdutoDto.Descricao, imagem);

        _repository.Criar(produto);
       
        await _repository.UnitOfWork.Commit();

        return produto;
    }
}
