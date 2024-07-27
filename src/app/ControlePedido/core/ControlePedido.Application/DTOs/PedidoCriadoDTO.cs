using System.ComponentModel;
using System.Text.Json.Serialization;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("PedidoCriado")]
    public class PedidoCriadoDTO
    {
        [JsonPropertyName("pedido")]
        public PedidoDTO Pedido { get; set; }

        [JsonPropertyName("qrCodePagamento")]
        public string QRCodePagamento { get; set; }

        public PedidoCriadoDTO()
        {
        }

        public PedidoCriadoDTO(Pedido pedido, string qRCodePagamento)
        {
            Pedido = new PedidoDTO(pedido);
            QRCodePagamento = qRCodePagamento;
        }
    }
}

