using ControlePedido.Api.Base;
using ControlePedido.Application.DTOs;
using ControlePedido.Application.UseCases.Clientes;
using ControlePedido.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedido.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : MainController
    {
        private readonly ILogger<ClienteController> _logger;
        
        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetClientes")]
        public async Task<ActionResult<ICollection<Cliente>>> Get([FromServices] IListarTodosClientesUseCase useCase)
        {
            var result = await useCase.Executar();
            return CustomResponse(result);
        }

        [HttpPost(Name = "PostCliente")]
        public async Task<ActionResult<Cliente>> Post([FromServices] ICriarClienteUseCase useCase, [FromBody] CriarClienteDTO cliente)
        {
            var result = await useCase.Executar(cliente.Nome, cliente.Cpf, cliente.Email);
            return CustomResponse(result);
        }
    }
}
