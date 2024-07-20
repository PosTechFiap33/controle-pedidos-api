using System.ComponentModel;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("PagarPedido")]
    public class PagarPedidoDTO
    {
        public string CodigoTransacao { get; private set; }
        public decimal ValorPago { get; private set; }

        public PagarPedidoDTO(string codigoTransacao, decimal valorPago)
        {
            CodigoTransacao = codigoTransacao;
            ValorPago = valorPago;
        }
    }

    [DisplayName("PagarPedidoManual")]
    public class PagarPedidoManualDTO : PagarPedidoDTO
    {
        public Guid PedidoId { get; private set; }

        public PagarPedidoManualDTO(Guid pedidoId, string codigoTransacao, decimal valorPago) : base(codigoTransacao, valorPago)
        {
            PedidoId = pedidoId;
        }
    }
}

