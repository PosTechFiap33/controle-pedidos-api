using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Bogus;
using ControlePedido.Application.DTOs;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;
using ControlePedido.Domain.ValueObjects;
using FluentAssertions;
using TechTalk.SpecFlow;
using Xunit;
using static ControlePedido.Domain.Entities.Pedido;

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
    private string _codigoTransacaoPagamento;
    private readonly IntegrationTestFixture _fixture;
    private List<Pedido> _pedidos;

    public PedidoStepDefinitions(IntegrationTestFixture fixture)
    {
        _client = fixture.Client;
        _pedido = new CriarPedidoDTO();
        _fixture = fixture;
    }

    [Given(@"que eu iforme o cpf do cliente ""(.*)""")]
    public void GivenQueEuInformeCpfCliente(string cpf)
    {
        _pedido.CpfCliente = cpf;
    }

    [Given(@"que eu adicione o produto de valor (.*)")]
    public async Task Givenqueeuadicioneoprodutodeidevalor(decimal value)
    {
        var produto = await AdicionarProdutosNoBanco(value);
        _pedido.Itens.Add(new PedidoItemDTO { ProdutoId = produto.Id });
    }


    [Given(@"que eu tenha pedidos cadastrados")]
    public async Task GivenQueEuTenhaPedidosCadastrados()
    {
        var pedidosRemover = _fixture.context.Pedido.ToList();
        _fixture.context.Pedido.RemoveRange(pedidosRemover);
        _fixture.context.SaveChanges();

        var produto = await AdicionarProdutosNoBanco(100);

        var itens = new List<PedidoItem>{
            new PedidoItem(produto)
        };

        var pedidoCriado = PedidoFactory.Criar(itens);

        var pedidoPago = PedidoFactory.Criar(itens);
        pedidoPago.Pagar(new Guid().ToString(), DateTime.Now, 100);

        var pedidoIniciado = PedidoFactory.Criar(itens);
        pedidoIniciado.Pagar(new Guid().ToString(), DateTime.Now, 100);
        pedidoIniciado.IniciarPreparo();

        var pedidoFinalizado = PedidoFactory.Criar(itens);
        pedidoFinalizado.Pagar(new Guid().ToString(), DateTime.Now, 100);
        pedidoFinalizado.IniciarPreparo();
        pedidoFinalizado.FinalizarPreparo();

        var pedidoEntregue = PedidoFactory.Criar(itens);
        pedidoEntregue.Pagar(new Guid().ToString(), DateTime.Now, 100);
        pedidoEntregue.IniciarPreparo();
        pedidoEntregue.FinalizarPreparo();
        pedidoEntregue.Finalizar();

        _pedidos = new List<Pedido>
        {
           pedidoCriado,
           pedidoPago,
           pedidoIniciado,
           pedidoFinalizado,
           pedidoEntregue
        };

        _fixture.context.Pedido.AddRange(_pedidos);
        await _fixture.context.SaveChangesAsync();
    }

    [When(@"eu fizer uma requisicao para gerar o pedido")]
    public async Task Wheneufizerumarequisicaoparageraropedido()
    {
        _response = await _client.PostAsJsonAsync(_rota, _pedido);
        _pedidoCriado = await RecuperarPedidoDaResposta();
    }

    [When(@"eu fizer uma requisicao para iniciar o preparo")]
    public async Task Wheneufizerumarequisicaoparainiciaropreparo()
    {
        _response = await _client.PatchAsync($"{_rota}/{_pedidoCriado.Pedido.Id}/iniciar-preparo", null);
    }

    [When(@"eu fizer uma requisicao para finalizar o prepado do pedido")]
    public async Task Wheneufizerumarequisicaoparafinalizaroprepadodopedido()
    {
        _response = await _client.PatchAsync($"{_rota}/{_pedidoCriado.Pedido.Id}/finalizar-preparo", null);
    }

    [When(@"eu fizer o pagamento manual do pedido criado")]
    public async Task Wheneufizeropagamentomanualdopedidocriado()
    {
        _codigoTransacaoPagamento = Guid.NewGuid().ToString();
        var pagamentoManual = new PagarPedidoManualDTO(_pedidoCriado.Pedido.Id, _codigoTransacaoPagamento, _pedidoCriado.Pedido.Valor);
        _response = await _client.PostAsJsonAsync($"api/pagamento", pagamentoManual);
    }

    [When(@"eu fizer uma requisicao para realizar a entrega do pedido")]
    public async Task Wheneufizerumarequisicaopararealizaraentregadopedido()
    {
        _response = await _client.PatchAsync($"{_rota}/{_pedidoCriado.Pedido.Id}/entregar", null);
    }

    [When(@"eu fizer uma requisicao listar os pedidos")]
    public async Task Wheneufizerumarequisicaolistarospedidos()
    {
        _response = await _client.GetAsync($"{_rota}");
    }

    [Then(@"o status code deve ser (.*)")]
    public void Thenostatuscodedeveser(HttpStatusCode statusCode)
    {
        _response.StatusCode.Should().Be(statusCode);
    }

    [Then(@"os dados do pedido estejam validos")]
    public async Task GivenOsDadosPedidoEstejamValidos()
    {
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

    [Then(@"os dados do pagamento devem estar preenchidos")]
    public void Thenosdadosdopagamentodevemestarpreenchidos()
    {
        _acompanhamentoPedido.DadosPagamento.DataHoraPagamento.Should().NotBe(DateTime.MinValue);
        _acompanhamentoPedido.DadosPagamento.CodigoTransacao.Should().Be(_codigoTransacaoPagamento);
        _acompanhamentoPedido.DadosPagamento.ValorPago.Should().Be(_pedidoCriado.Pedido.Valor);
    }

    [Then(@"deve ser exibida a mensagem de erro ""(.*)""")]
    public async Task Thendeveserexibidaamensagemdeerro(string erro)
    {
        await _fixture.TestarRequisicaoComErro(_response, new List<string> { erro });
    }

    [Then(@"deve ser exibida a lista dos pedidos")]
    public async Task Thendeveserexibidaalistadospedidos()
    {
        var statusDesejados = new List<string> {
               "Pronto",
               "Em preparacao",
               "Recebido"
             };

        var dados = await _response.Content.ReadAsStringAsync();
        var pedidos = JsonSerializer.Deserialize<List<PedidoDTO>>(dados);

        pedidos.Should().NotBeEmpty();
        pedidos[0].Status.Should().Be("Pronto");
        pedidos[0].Should().BeEquivalentTo(pedidos.Where(p => p.Status == "Pronto").FirstOrDefault());

        pedidos[1].Status.Should().Be("Em preparacao");
        pedidos[1].Should().BeEquivalentTo(pedidos.Where(p => p.Status == "Em preparacao").FirstOrDefault());

        pedidos[2].Status.Should().Be("Recebido");
        pedidos[2].Should().BeEquivalentTo(pedidos.Where(p => p.Status == "Recebido").FirstOrDefault());
    }

    private async Task<Produto> AdicionarProdutosNoBanco(decimal valor)
    {
        var db = _fixture.context;

        var fakerImagem = new Faker<Imagem>()
           .CustomInstantiator(f => new Imagem(
               f.Internet.Url(),
               f.Random.String2(3, 3),
               f.Commerce.ProductName()
           ));

        var fakerProduto = new Faker<Produto>()
            .CustomInstantiator(f => new Produto(
                f.Commerce.ProductName(),
                valor,
                f.PickRandom<Categoria>(),
                f.Lorem.Sentence(),
                fakerImagem.Generate()
            ));

        var produto = fakerProduto.Generate();

        db.Produto.Add(produto);
        await db.SaveChangesAsync();

        return produto;
    }

    private async Task<PedidoCriadoDTO> RecuperarPedidoDaResposta()
    {
        var dados = await _response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PedidoCriadoDTO>(dados);
    }
}
