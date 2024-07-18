using System.Net;
using ControlePedido.Api.Base;
using ControlePedido.Application.DTOs;
using ControlePedido.Application.UseCases.Clientes;
using ControlePedido.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedido.Api.Controllers
{
    /// <summary>
    /// Controlador para gerenciamento de clientes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : MainController
    {
        public ClienteController(ILogger<ClienteController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Obtém uma lista de todos os clientes cadastrados no sistema.
        /// </summary>
        /// <remarks>
        /// Retorna todos os clientes registrados no sistema, sem filtros.
        /// </remarks>
        /// <param name="useCase">A instância do caso de uso para listar todos os clientes.</param>
        /// <returns>Uma lista de clientes cadastrados no sistema.</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<ClienteDTO>))]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(500, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult<ICollection<ClienteDTO>>> Get([FromServices] IListarTodosClientesUseCase useCase)
        {
            var result = await useCase.Executar();
            return CustomResponse(result);
        }

        /// <summary>
        /// Cria um novo cliente com os dados fornecidos.
        /// </summary>
        /// <remarks>
        /// Cria um novo cliente no sistema com base nos dados fornecidos no corpo da solicitação.
        /// Retorna o cliente recém-criado, incluindo seu identificador único (ID).
        /// </remarks>
        /// <param name="useCase">A instância do caso de uso para criar o cliente.</param>
        /// <param name="cliente">Os dados do cliente a serem criados.</param>
        /// <returns>O cliente recém-criado.</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Guid))]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(500, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult<Guid>> Post([FromServices] ICriarClienteUseCase useCase, [FromBody] CriarClienteDTO cliente)
        {
            var result = await useCase.Executar(cliente.Nome, cliente.Cpf, cliente.Email);
            return CustomResponse(result, HttpStatusCode.Created);
        }
    }
}
