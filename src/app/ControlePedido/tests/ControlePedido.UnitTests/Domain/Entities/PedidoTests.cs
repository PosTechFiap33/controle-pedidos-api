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
        private Cliente cliente;
        private ICollection<PedidoItem> itens;

        public PedidoTestes()
        {
            var imagem = new Imagem("http://teste", "jpg", "teste");

            itens = new List<PedidoItem> {
               new PedidoItem(new Produto("teste", 10, Categoria.Acompanhamento, "teste", imagem))
            };

            cliente = new Cliente("Teste", "12345678909", "teste@teste.com");
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
            var pedido = PedidoFactory.Criar(itens, cliente);

            Assert.Equal(cliente.Id, pedido.Cliente.Id);
            Assert.Equal(cliente, pedido.Cliente);
            Assert.Equal(StatusPedido.CRIADO, pedido.RetornarStatusAtual());
            Assert.Equal(itens, pedido.Itens);
        }

        [Fact(DisplayName = "Deve retornar mensagem 'O código da transação não pode ser vazio!' ao realizar um pagamento")]
        public void Pagar_DeveLancarExcecaoQuandoNaoInformarCodigoTransacao()
        {
            var codigoTransacao = "";

            var pedido = PedidoFactory.Criar(itens, cliente);

            var excecao = Assert.Throws<DomainException>(() => pedido.Pagar(codigoTransacao));

            Assert.Equal("O código da transação não pode ser vazio!", excecao.Message);
            Assert.Equal(StatusPedido.CRIADO, pedido.RetornarStatusAtual());
        }

        [Fact(DisplayName = "Deve realizar um pagamento de pedido com sucesso")]
        public void Pagar_DeveCriarPagamentoQuandoCodigoTransacaoEhVazio()
        {
            var codigoTransacao = "123456";
            var pedido = PedidoFactory.Criar(itens, cliente);

            pedido.Pagar(codigoTransacao);

            Assert.NotNull(pedido.Pagamento);
            Assert.Equal(codigoTransacao, pedido.Pagamento.CodigoTransacao);
            Assert.Equal(StatusPedido.RECEBIDO, pedido.RetornarStatusAtual());
        }

        [Theory(DisplayName = "Deve alterar o status apenas uma vez ao pagar pedido!")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Pagar_DeveInserirStatusApenasUmaVez(int quantidadeExecucao)
        {
            var codigoTransacao = "123456";
            var pedido = PedidoFactory.Criar(itens, cliente);

            for (var i = 0; i < quantidadeExecucao; i++)
                pedido.Pagar(codigoTransacao);

            Assert.Equal(2, pedido.Status.Count);
        }
        
        [Fact(DisplayName = "Deve retornar mensagem de pagamento nao realizado ao iniciar o preparo")]
        public void IniciarPreparo_DeveRetornarMensagemPagamentoNaoRealizado()
        {
            var pedido = PedidoFactory.Criar(itens, cliente);

            var excecao = Assert.Throws<DomainException>(() => pedido.IniciarPreparo());

            Assert.Equal("Não foi realizado o pagamento para o pedido informado!", excecao.Message);
            Assert.Equal(StatusPedido.CRIADO, pedido.RetornarStatusAtual());
        }

        [Fact(DisplayName = "Deve iniciar o preparo do pedido quando pagamento realizado")]
        public void IniciarPreparo_DeveAtualizarStatusParaEmPreparacaoQuandoPagamentoRealizado()
        {
            var pedido = PedidoFactory.Criar(itens, cliente);
            pedido.Pagar("123456");

            pedido.IniciarPreparo();

            Assert.Equal(StatusPedido.EM_PREPARACAO, pedido.RetornarStatusAtual());
        }

        [Fact(DisplayName = "Deve retornar mensagem 'Não foi possível finalizar o preparo do pedido pois o preparo não foi iniciado!' ao finalizar preparo!")]
        public void FinalizarPreparo_DeveRetornarMensagemPreparoPedidoNaoFinalizado()
        {
            var pedido = PedidoFactory.Criar(itens, cliente);
            pedido.Pagar("123456");

            var excecao = Assert.Throws<DomainException>(() => pedido.FinalizarPreparo());

            Assert.Equal("Não foi possível finalizar o preparo do pedido pois o preparo não foi iniciado!", excecao.Message);
            Assert.Equal(StatusPedido.RECEBIDO, pedido.RetornarStatusAtual());
        }

        [Fact(DisplayName = "Deve finalizar o preparo do pedido com sucesso apos preparo iniciado!")]
        public void FinalizarPreparo_DeveAtualizarStatusParaProntoQuandoPreparoIniciado()
        {
            var pedido = PedidoFactory.Criar(itens, cliente);
            pedido.Pagar("123456");
            pedido.IniciarPreparo();

            pedido.FinalizarPreparo();

            Assert.Equal(StatusPedido.PRONTO, pedido.RetornarStatusAtual());
        }

        [Fact(DisplayName = "Deve retornar mensagem 'Não foi possível finalizar o pedido pois o preparo não foi finalizado!' ao finalizar o pedido")]
        public void Finalizar_DeveRetornarMensagemPreparoNaoFinalizado()
        {
            var pedido = PedidoFactory.Criar(itens, cliente);
            pedido.Pagar("123456");
          
            var excecao = Assert.Throws<DomainException>(() => pedido.Finalizar());

            Assert.Equal("Não foi possível finalizar o pedido pois o preparo não foi finalizado!", excecao.Message);
            Assert.Equal(StatusPedido.RECEBIDO, pedido.RetornarStatusAtual());
        }

        [Fact(DisplayName = "Deve finalizar o pedido com sucesso!")]
        public void Finalizar_DeveAtualizarStatusParaFinalizadoQuandoPreparoFinalizado()
        {
            var pedido = PedidoFactory.Criar(itens, cliente);
            pedido.Pagar("123456");
            pedido.IniciarPreparo();
            pedido.FinalizarPreparo();

            pedido.Finalizar();

            Assert.Equal(StatusPedido.FINALIZADO, pedido.RetornarStatusAtual());
        }
    }
}
