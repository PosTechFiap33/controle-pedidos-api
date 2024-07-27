using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ControlePedido.Application.DTOs;
using ControlePedido.Domain.Entities;
using FluentAssertions;
using TechTalk.SpecFlow;
using Xunit;

namespace ControlePedido.IntegrationTests;

[Binding]
public class PedidoStepDefinitions : IClassFixture<IntegrationTestFixture>
{
    private readonly CriarPedidoDTO _pedido;
    private readonly HttpClient _client;
    private readonly string _rota = "api/pedido";
    private HttpResponseMessage _response;
    private PedidoCriadoDTO _pedidoCriado;
    private AcompanhamentoPedidoDTO _acompanhamentoPedido;
    private string _cpfVinculado;

    public PedidoStepDefinitions(IntegrationTestFixture fixture)
    {
        _client = fixture.Client;
        _pedido = new CriarPedidoDTO();
    }

    [Given(@"que eu iforme o cpf do cliente ""(.*)""")]
    public void Givenqueeuiformeocpfdocliente(string cpf)
    {
        _pedido.CpfCliente = cpf;
    }

    [Given(@"que eu adicione o produto de id ""(.*)""")]
    public void Givenqueeuadicioneoprodutodeid(Guid produtoId)
    {
        _pedido.Itens.Add(new PedidoItemDTO { ProdutoId = produtoId });
    }

    [When(@"eu fizer uma requisicao para gerar o pedido")]
    public async Task Wheneufizerumarequisicaoparageraropedido()
    {
        _response = await _client.PostAsJsonAsync(_rota, _pedido);
    }

    [Then(@"o status code deve ser (.*)")]
    public void Thenostatuscodedeveser(HttpStatusCode statusCode)
    {
        _response.StatusCode.Should().Be(statusCode);
    }

    [Then(@"os dados do pedido estejam validos")]
    public async Task GivenOsDadosPedidoEstejamValidos()
    {
        var dados = await _response.Content.ReadAsStringAsync();
        _pedidoCriado = JsonSerializer.Deserialize<PedidoCriadoDTO>(dados);
        _pedidoCriado.Pedido.Id.Should().NotBe(Guid.Empty);
        _pedidoCriado.Pedido.Itens.Should().Contain(p => _pedido.Itens.Any(x => x.ProdutoId == p.Id));
        _pedidoCriado.Pedido.Status.Should().Be("Criado");
        _pedidoCriado.QRCodePagamento.Should().NotBeEmpty();
    }

    [Then(@"o cpf vinculado no pedido deve ser ""(.*)""")]
    public void Thenocpfvinculadonopedidodeveser(string cpf)
    {
        _cpfVinculado = cpf;
        _pedidoCriado.Pedido.CpfCliente.Should().Be(cpf);
    }

    [Then(@"o valor do pedido deve ser (.*)")]
    public void Thenovalordopedidodeveser(decimal valor)
    {
        _pedidoCriado.Pedido.Valor.Should().Be(valor);
    }

    [Then(@"o status do pedido deve ser ""(.*)""")]
    public async Task Thenostatusdopedidodeveser(string status)
    {
        var response = await _client.GetAsync($"{_rota}/{_pedidoCriado.Pedido.Id}/acompanhar");
        var dados = await response.Content.ReadAsStringAsync();
        _acompanhamentoPedido = JsonSerializer.Deserialize<AcompanhamentoPedidoDTO>(dados);
        _acompanhamentoPedido.CpfCliente.Should().Be(_cpfVinculado);
        _acompanhamentoPedido.Status.Should().Be(status);
        _acompanhamentoPedido.Valor.Should().Be(_pedidoCriado.Pedido.Valor);
    }

    [Then(@"os dados do pagamento devem estar vazios")]
    public void Thenosdadosdopagamentodevemestarvazios()
    {
        _acompanhamentoPedido.DadosPagamento.Should().BeNull();
    }

}
