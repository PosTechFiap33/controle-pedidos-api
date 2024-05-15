using ControlePedido.Application.UseCases.Produtos;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedido.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase
{
    [HttpPost(Name = "PostProduto")]
    public async Task<ActionResult<Produto>> Post([FromServices] ICriarProdutoUseCase useCase, [FromBody] CriarProdutoDto produto)
    {
        var result = await useCase.Executar(produto);
        return Ok(result);
    }

    [HttpGet(Name = "GetProdutoPorCategoria")]
    public async Task<ActionResult<List<Produto>>> GetPorCategoria([FromServices] IListarProdutoPorCategoriaUseCase useCase, [FromQuery] Categoria categoria){
        var result = await useCase.Executar(categoria);
        return Ok(result);
    }

}
