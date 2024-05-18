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
            ProdutoId = produto.Id;
            Produto = produto;
            ValidateEntity();
        }

        private void ValidateEntity()
        {
           AssertionConcern.AssertArgumentNotEquals(Guid.Empty, ProdutoId, "Codigo do produto não foi informado!");
        }
    }

    public class PedidoPagamento : Entity{
        public Guid PedidoId { get; private set; }
        public DateTime DataHoraPagamento { get; private set; }
        public string CodigoTransacao { get; private set; }
        public virtual Pedido Pedido { get; private set; }

        public PedidoPagamento(string codigoTransacao)
        {
            DataHoraPagamento = DateTime.Now;
            CodigoTransacao = codigoTransacao;
        }
    }

    public class Pedido : Entity, IAggregateRoot
    {
        public decimal Valor { get; private set; }
        public Guid ClienteId { get; private set; }
        public StatusPedido Status { get; private set; }
        public DateTime DataHoraCriacao { get; private set; }
        public DateTime DataHoraInicio { get; private set; }
        public DateTime DataHoraFim { get; private set; }
        public virtual ICollection<PedidoItem> Itens { get; private set; }
        public virtual PedidoPagamento Pagamento { get; private set; }
        public virtual Cliente Cliente { get; private set; }

        protected Pedido() {}

        public Pedido(ICollection<PedidoItem> itens)
        {
            Itens = itens;
            DataHoraCriacao = DateTime.UtcNow;
            Valor = Itens.Sum(i => i.Produto.Preco);
            ValidateEntity();
        }

        public Pedido(ICollection<PedidoItem> itens, Guid clienteId) : this(itens)
        {
            ClienteId = clienteId;
            AssertionConcern.AssertArgumentNotEquals(Guid.Empty, ClienteId, "Codigo do cliente não foi informado!");
        }

        public void Pagar(string codigoTransacao){
            Status = StatusPedido.RECEBIDO;
            Pagamento = new PedidoPagamento(codigoTransacao);
        }

        public void IniciarPreparo()
        {
            Status = StatusPedido.EM_PREPARACAO;
            DataHoraInicio = DateTime.UtcNow;
        }

        public void FinalizarPreparo()
        {
           Status = StatusPedido.PRONTO;
        }

        public void Finalizar()
        {
            Status = StatusPedido.FINALIZADO;
            DataHoraFim = DateTime.UtcNow;
        }

        private void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEquals(Itens.Any(), false, "O Pedido deve conter pelo nenos 1 item!");
        }
    }
}