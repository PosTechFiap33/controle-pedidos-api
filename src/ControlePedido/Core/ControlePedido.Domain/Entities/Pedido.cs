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
        public virtual Pedido Pedido { get; set; }
        public virtual Produto Produto { get; set; }

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
        public virtual Pedido Pedido { get; private set; }

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
        public virtual Pedido Pedido { get; private set; }

        public PedidoStatus(StatusPedido status)
        {
            Status = status;
            DataHora = DateTime.UtcNow;
        }
    }

    public class Pedido : Entity, IAggregateRoot
    {
        public decimal Valor { get; private set; }
        public Guid ClienteId { get; private set; }
        public virtual ICollection<PedidoItem> Itens { get; private set; }
        public virtual ICollection<PedidoStatus> Status { get; private set; }
        public virtual PedidoPagamento Pagamento { get; private set; }
        public virtual Cliente Cliente { get; private set; }

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

        private Pedido(ICollection<PedidoItem> itens, Guid clienteId) : this(itens)
        {
            ClienteId = clienteId;
            AssertionConcern.AssertArgumentNotEquals(Guid.Empty, ClienteId, "Codigo do cliente não foi informado!");
        }

        public void Pagar(string codigoTransacao)
        {
            AssertionConcern.AssertArgumentNotEmpty(codigoTransacao, "O código da transação não pode ser vazio!");

            if (Pagamento is null)
            {
                Status.Add(new PedidoStatus(StatusPedido.RECEBIDO));
                Pagamento = new PedidoPagamento(codigoTransacao);
            }
        }

        public void IniciarPreparo()
        {
            AtualizarStatus(StatusPedido.EM_PREPARACAO);
        }

        public void FinalizarPreparo()
        {
            AtualizarStatus(StatusPedido.PRONTO);
        }

        public void Finalizar()
        {
            AtualizarStatus(StatusPedido.FINALIZADO);
        }

        private void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEquals(Itens.Any(), false, "O Pedido deve conter pelo nenos 1 item!");
            AssertionConcern.AssertGratherThanValue(Valor, 0, "O valor do pedido deve ser maior que 0!");
        }

        private void AtualizarStatus(StatusPedido status)
        {
            if (Status.Any(s => s.Status != status))
                Status.Add(new PedidoStatus(status));
        }

        public static class PedidoFactory
        {
            public static Pedido Criar(ICollection<PedidoItem> itens, Cliente? cliente = null)
            {
                return cliente is null ? new Pedido(itens) : new Pedido(itens, cliente.Id);
            }
        }
    }
}