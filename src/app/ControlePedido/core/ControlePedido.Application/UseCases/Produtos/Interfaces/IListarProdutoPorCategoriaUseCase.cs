using ControlePedido.Application.DTOs;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Application.UseCases.Produtos;

public interface IListarProdutoPorCategoriaUseCase
{
    Task<ICollection<ProdutoDTO>> Executar(Categoria categoria);
}
