using System;
using System.Collections.Generic;
using System.Linq;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Domain.Entities
{
    public class PedidoItem : Entity
    {
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }

        protected PedidoItem() { }

        public PedidoItem(Produto produto)
        {
            Produto = produto;
            AssertionConcern.AssertArgumentNotNull(Produto, "O Produto deve ser informado ao criar um item de pedido!");

            ProdutoId = produto.Id;
            AssertionConcern.AssertArgumentNotEquals(Guid.Empty, ProdutoId, "Codigo do produto não foi informado!");
        }
    }

    public class PedidoPagamento : Entity
    {
        public Guid PedidoId { get; private set; }
        public DateTime DataHoraPagamento { get; private set; }
        public string CodigoTransacao { get; private set; }
        public Pedido Pedido { get; private set; }

        public PedidoPagamento(string codigoTransacao)
        {
            DataHoraPagamento = DateTime.UtcNow;
            CodigoTransacao = codigoTransacao;
        }
    }

    public class PedidoStatus : Entity
    {
        public Guid PedidoId { get; private set; }
        public StatusPedido Status { get; private set; }
        public DateTime DataHora { get; private set; }
        public Pedido Pedido { get; private set; }

        public PedidoStatus(StatusPedido status)
        {
            Status = status;
            DataHora = DateTime.UtcNow;
        }
    }

    public class Pedido : Entity, IAggregateRoot
    {
        public decimal Valor { get; private set; }
        public Guid? ClienteId { get; private set; }
        public ICollection<PedidoItem> Itens { get; private set; }
        public ICollection<PedidoStatus> Status { get; private set; }
        public PedidoPagamento Pagamento { get; private set; }
        public Cliente Cliente { get; private set; }

        protected Pedido() { }

        private Pedido(ICollection<PedidoItem> itens)
        {
            Itens = itens;
            Status = new List<PedidoStatus> {
                new PedidoStatus(StatusPedido.CRIADO)
            };
            Valor = Itens.Sum(i => i.Produto.Preco);
            ValidateEntity();
        }

        private Pedido(ICollection<PedidoItem> itens, Cliente cliente) : this(itens)
        {
            Cliente = cliente;
            ClienteId = cliente.Id;
            AssertionConcern.AssertArgumentNotEquals(Guid.Empty, ClienteId, "Codigo do cliente não foi informado!");
        }

        public void Pagar(string codigoTransacao)
        {
            AssertionConcern.AssertArgumentNotEmpty(codigoTransacao, "O código da transação não pode ser vazio!");

            if (Pagamento is null)
            {
                Pagamento = new PedidoPagamento(codigoTransacao);
                AtualizarStatus(StatusPedido.RECEBIDO);
            }
        }

        public void IniciarPreparo()
        {
            AssertionConcern.AssertArgumentTrue(ValidarPagamento(), "Não foi realizado o pagamento para o pedido informado!");
            AtualizarStatus(StatusPedido.EM_PREPARACAO);
        }

        public void FinalizarPreparo()
        {
            var preparoPedidoIniciado = VerificarExisteStatus(StatusPedido.EM_PREPARACAO);

            AssertionConcern.AssertArgumentTrue(preparoPedidoIniciado, "Não foi possível finalizar o preparo do pedido pois o preparo não foi iniciado!");

            AtualizarStatus(StatusPedido.PRONTO);
        }

        public void Finalizar()
        {
            var preparoPedidoIniciado = VerificarExisteStatus(StatusPedido.PRONTO);

            AssertionConcern.AssertArgumentTrue(preparoPedidoIniciado, "Não foi possível finalizar o pedido pois o preparo não foi finalizado!");

            AtualizarStatus(StatusPedido.FINALIZADO);
        }

        public StatusPedido RetornarStatusAtual()
        {
            return Status.OrderByDescending(p => p.DataHora)
                         .FirstOrDefault()
                         .Status;
        }

        private void AtualizarStatus(StatusPedido status)
        {
            AssertionConcern.AssertArgumentNotNull(Pagamento, "Para avançar com o pedido é necessário realizar o pagamento!");

            if (Status.Any(s => s.Status == status))
                return;

            Status.Add(new PedidoStatus(status));
        }

        private bool ValidarPagamento()
        {
            return Pagamento != null && Pagamento.Id != Guid.Empty;
        }

        private bool VerificarExisteStatus(StatusPedido status)
        {
            return Status.Any(s => s.Status == status);
        }

        private void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEquals(Itens.Any(), false, "O Pedido deve conter pelo nenos 1 item!");
            AssertionConcern.AssertGratherThanValue(Valor, 0, "O valor do pedido deve ser maior que 0!");
        }

        public static class PedidoFactory
        {
            public static Pedido Criar(ICollection<PedidoItem> itens, Cliente? cliente = null)
            {
                return cliente is null ? new Pedido(itens) : new Pedido(itens, cliente);
            }
        }
    }
}