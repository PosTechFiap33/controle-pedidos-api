using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("PagarPedido")]
    public class PagarPedidoDTO
    {
        [JsonPropertyName("codigoTransacao")]
        public string CodigoTransacao { get; set; }

        [JsonPropertyName("valorPago")]
        public decimal ValorPago { get; set; }

        public PagarPedidoDTO(string codigoTransacao, decimal valorPago)
        {
            CodigoTransacao = codigoTransacao;
            ValorPago = valorPago;
        }
    }

    [DisplayName("PagarPedidoManual")]
    public class PagarPedidoManualDTO : PagarPedidoDTO
    {
        [JsonPropertyName("pedidoId")]
        public Guid PedidoId { get; set; }

        public PagarPedidoManualDTO(Guid pedidoId, string codigoTransacao, decimal valorPago) : base(codigoTransacao, valorPago)
        {
            PedidoId = pedidoId;
        }
    }
}

