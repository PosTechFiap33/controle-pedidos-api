using System;

namespace ControlePedido.Domain.Adapters.DTOs
{

    public class PagamentoRealizadoDTO
    {
        public Guid PedidoId { get; set; }
        public DateTime DataPagamento { get; set; }
        public string CodigoTransacao { get; set; }
        public decimal ValorPago { get; set; }
    }
}