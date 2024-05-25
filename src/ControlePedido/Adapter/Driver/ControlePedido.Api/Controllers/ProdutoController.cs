using ControlePedido.Api.Base;
using ControlePedido.Application.DTOs;
using ControlePedido.Application.UseCases.Produtos;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedido.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : MainController
{
    [HttpPost(Name = "PostProduto")]
    public async Task<ActionResult<Guid>> Post([FromServices] ICriarProdutoUseCase useCase, [FromBody] CriarProdutoDTO produto)
    {
        var result = await useCase.Executar(produto);
        return CustomResponse(result);
    }

    [HttpGet(Name = "GetProdutoPorCategoria")]
    public async Task<ActionResult<ICollection<Produto>>> GetPorCategoria([FromServices] IListarProdutoPorCategoriaUseCase useCase, [FromQuery] Categoria categoria){
        var result = await useCase.Executar(categoria);
        return CustomResponse(result);
    }

}
