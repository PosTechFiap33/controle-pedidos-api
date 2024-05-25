using System.Net;
using ControlePedido.Api.Base;
using ControlePedido.Application.DTOs;
using ControlePedido.Application.UseCases.Produtos;
using ControlePedido.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedido.Api.Controllers;

/// <summary>
/// Controlador para gerenciamento de produto.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProdutoController : MainController
{
    /// <summary>
    /// Cria um novo produto com base nos dados fornecidos.
    /// </summary>
    /// <remarks>
    /// Cria um novo produto no sistema com base nos dados fornecidos no corpo da solicitação.
    /// Retorna o identificador único (ID) do produto criado.
    /// </remarks>
    /// <param name="useCase">A instância do caso de uso para criar o produto.</param>
    /// <param name="produto">Os dados do produto a serem criados.</param>
    /// <returns>O identificador único (ID) do produto recém-criado.</returns>
    [HttpPost(Name = "PostProduto")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(500, Type = typeof(ValidationProblemDetails))]
    public async Task<ActionResult<Guid>> Post([FromServices] ICriarProdutoUseCase useCase, [FromBody] CriarProdutoDTO produto)
    {
        var result = await useCase.Executar(produto);
        return CustomResponse(result, HttpStatusCode.Created);
    }


    /// <summary>
    /// Obtém uma lista de produtos com base na categoria especificada.
    /// </summary>
    /// <remarks>
    /// Retorna uma lista de produtos que pertencem à categoria fornecida.
    /// </remarks>
    /// <param name="useCase">A instância do caso de uso para listar os produtos por categoria.</param>
    /// <param name="categoria">A categoria pela qual filtrar os produtos.</param>
    /// <returns>Uma lista de produtos que pertencem à categoria fornecida.</returns>
    [HttpGet(Name = "GetProdutoPorCategoria")]
    [ProducesResponseType(200, Type = typeof(ICollection<ProdutoDTO>))]
    [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(500, Type = typeof(ValidationProblemDetails))]
    public async Task<ActionResult<ICollection<ProdutoDTO>>> GetPorCategoria([FromServices] IListarProdutoPorCategoriaUseCase useCase, [FromQuery] Categoria categoria)
    {
        var result = await useCase.Executar(categoria);
        return CustomResponse(result);
    }

}
