using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;
using ControlePedido.Domain.ValueObjects;
using Moq;
using static ControlePedido.Domain.Entities.Pedido;

namespace ControlePedido.Domain.Testes
{
    public class PedidoTestes
    {
        private Pedido pedido;

        public PedidoTestes()
        {
        }

        [Fact(DisplayName = "Deve retornar mensagem 'O Pedido deve conter pelo nenos 1 item!' ao instanciar um pedido sem item")]
        public void Criar_DeveLancarExcecaoQuandoCriarPedidoSemItens()
        {
            var excecao = Assert.Throws<DomainException>(() => PedidoFactory.Criar(new List<PedidoItem>()));
            Assert.Equal("O Pedido deve conter pelo nenos 1 item!", excecao.Message);
        }

        [Fact(DisplayName = "Deve retornar mensagem 'O valor do pedido deve ser maior que 0!' ao instanciar um pedido com valor menor igual a 0")]
        public void Criar_DeveLancarExcecaoQuandoValorDoPedidoForMenorIgualZero()
        {
            var produto = new Mock<Produto>();

            typeof(Produto).GetProperty("Preco").SetValue(produto.Object, 0m);

            var itens = new List<PedidoItem> {
                new PedidoItem(produto.Object)
            };

            var excecao = Assert.Throws<DomainException>(() => PedidoFactory.Criar(itens));
            Assert.Equal("O valor do pedido deve ser maior que 0!", excecao.Message);
        }

        [Fact(DisplayName = "Deve retornar mensagem 'Codigo do cliente não foi informado!' ao informar um cliente sem id")]
        public void Criar_DeveLancarExcecaoQuandoInformarClienteSemID()
        {
            var imagem = new Imagem("http://teste", "jpg", "teste");

            var itens = new List<PedidoItem> {
               new PedidoItem(new Produto("teste", 10, Categoria.Acompanhamento, "teste", imagem))
            };

            var clientSemId = new Mock<Cliente>();
            typeof(Cliente).GetProperty("Id").SetValue(clientSemId.Object, Guid.Empty);

            var excecao = Assert.Throws<DomainException>(() => PedidoFactory.Criar(itens, clientSemId.Object));
            Assert.Equal("Codigo do cliente não foi informado!", excecao.Message);
        }

        [Fact(DisplayName = "Deve criar um pedido com sucesso")]
        public void Criar_DeveCriarPedidoComSucesso()
        {
            var imagem = new Imagem("http://teste", "jpg", "teste");

            var itens = new List<PedidoItem> {
               new PedidoItem(new Produto("teste", 10, Categoria.Acompanhamento, "teste", imagem))
            };

            var cliente = new Cliente("Teste", "12345678909", "teste@teste.com");

            var pedido = PedidoFactory.Criar(itens, cliente);

            Assert.Equal(cliente.Id, pedido.Cliente.Id);
            Assert.Equal(cliente, pedido.Cliente);
            Assert.Equal(StatusPedido.CRIADO, pedido.RetornarStatusAtual());
            Assert.Equal(itens, pedido.Itens);
        }

        [Fact]
        public void Pagar_DeveCriarPagamentoQuandoCodigoTransacaoNaoEhVazio()
        {
            // Arrange
            var codigoTransacao = "123456";

            // Act
            pedido.Pagar(codigoTransacao);

            // Assert
            Assert.NotNull(pedido.Pagamento);
            Assert.Equal(codigoTransacao, pedido.Pagamento.CodigoTransacao);
            Assert.Equal(StatusPedido.RECEBIDO, pedido.RetornarStatusAtual());
        }

        [Fact]
        public void IniciarPreparo_DeveAtualizarStatusParaEmPreparacaoQuandoPagamentoRealizado()
        {
            // Arrange
            pedido.Pagar("123456");

            // Act
            pedido.IniciarPreparo();

            // Assert
            Assert.Equal(StatusPedido.EM_PREPARACAO, pedido.RetornarStatusAtual());
        }

        [Fact]
        public void FinalizarPreparo_DeveAtualizarStatusParaProntoQuandoPreparoIniciado()
        {
            // Arrange
            pedido.Pagar("123456");
            pedido.IniciarPreparo();

            // Act
            pedido.FinalizarPreparo();

            // Assert
            Assert.Equal(StatusPedido.PRONTO, pedido.RetornarStatusAtual());
        }

        [Fact]
        public void Finalizar_DeveAtualizarStatusParaFinalizadoQuandoPreparoFinalizado()
        {
            // Arrange
            pedido.Pagar("123456");
            pedido.IniciarPreparo();
            pedido.FinalizarPreparo();

            // Act
            pedido.Finalizar();

            // Assert
            Assert.Equal(StatusPedido.FINALIZADO, pedido.RetornarStatusAtual());
        }
    }
}
