using ControlePedido.Application.UseCases.Clientes;
using ControlePedido.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

public class ClienteViewModel
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
}

namespace ControlePedido.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        
        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostCliente")]
        public async Task<ActionResult<Cliente>> Post([FromServices] ICriarClienteUseCase useCase, [FromBody] ClienteViewModel cliente)
        {
            var result = await useCase.Executar(cliente.Nome, cliente.Cpf, cliente.Email);
            return Ok(result);
        }
    }
}
