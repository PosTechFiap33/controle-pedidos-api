using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ControlePedido.Application.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TechTalk.SpecFlow;
using Xunit;

namespace ControlePedido.IntegrationTests.Features
{
    [Binding]
    public class CadastrarClienteStepDefinitions : IClassFixture<IntegrationTestFixture>
    {
        private readonly string _rota = "api/cliente";
        private readonly HttpClient _client;
        private readonly CriarClienteDTO _cliente;
        private HttpResponseMessage _response;

        public CadastrarClienteStepDefinitions(IntegrationTestFixture fixture)
        {
            _client = fixture.Client;
            _cliente = new CriarClienteDTO();
        }

        [Given(@"que eu iforme o nome ""(.*)""")]
        public void Givenqueeuiformeonome(string nome)
        {
            _cliente.Nome = nome;
        }

        [Given(@"o email ""(.*)""")]
        public void Givenoemail(string email)
        {
            _cliente.Email = email;
        }

        [Given(@"o cpf ""(.*)""")]
        public void Givenocpf(string cpf)
        {
            _cliente.Cpf = cpf;
        }

        [When(@"for realizada uma consulta com o id de cliente cadastrado")]
        public async Task Givenforrealizadaumaconsultacomoiddeclientecadastrado()
        {
            _response = await _client.GetAsync(_rota);
        }

        [When(@"for feita a requisição para a rota de cadastro")]
        public async Task Whenforfeitaarequisioparaarotadecadastro()
        {
            _response = await _client.PostAsJsonAsync(_rota, _cliente);
        }

        [Then(@"deverá ser retornado o status (.*)")]
        public async void Thendeverserretornadoostatus(HttpStatusCode expectedStatus)
        {
            _response.StatusCode.Should().Be(expectedStatus);
        }

        [Then(@"o id do cliente deve ser válido")]
        public async Task Givenoiddoclientedeveservlido()
        {
            var dados = await _response.Content.ReadAsStringAsync();
            var idCliente = JsonSerializer.Deserialize<Guid>(dados);
            idCliente.Should().NotBe(Guid.Empty);
        }

        [Then(@"devera ser retornado os dados do cliente cadastrado")]
        public async Task Thendeveraserretornadoosdadosdoclientecadastrado()
        {
            var dados = await _response.Content.ReadAsStringAsync();
            var clientes = JsonSerializer.Deserialize<ICollection<ClienteDTO>>(dados);
            clientes.Should().ContainEquivalentOf(new ClienteDTO(_cliente.Nome, _cliente.Cpf, _cliente.Email));
        }

        [Then(@"a mensagem de erro deve ser ""(.*)""")]
        public async Task Givenamensagemdeerrodeveser(string erro)
        {
            var erros = new List<string> { erro };

            var dados = await _response.Content.ReadAsStringAsync();
            var errorDetail = JsonSerializer.Deserialize<ValidationProblemDetails>(dados);

            new ValidationProblemDetails(new Dictionary<string, string[]> {
                { "Mensagens", erros.ToArray() }
            });

            errorDetail.Errors["Mensagens"].Should().BeEquivalentTo(erros);
        }
        

    }
}

