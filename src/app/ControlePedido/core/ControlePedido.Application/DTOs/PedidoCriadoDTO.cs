using System.ComponentModel;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("PedidoCriado")]
    public class PedidoCriadoDTO
	{
        public PedidoDTO Pedido { get; private set; }
        public string QRCodePagamento { get; private set; }

        public PedidoCriadoDTO(Pedido pedido, string qRCodePagamento)
        {
            Pedido = new PedidoDTO(pedido);
            QRCodePagamento = qRCodePagamento;
        }
	}
}

