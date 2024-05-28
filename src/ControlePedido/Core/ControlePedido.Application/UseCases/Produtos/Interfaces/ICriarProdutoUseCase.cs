using ControlePedido.Application.DTOs;

namespace ControlePedido.Application.UseCases.Produtos;

public interface ICriarProdutoUseCase
{
    Task<Guid> Executar(CriarProdutoDTO criarProdutoDto);
}
