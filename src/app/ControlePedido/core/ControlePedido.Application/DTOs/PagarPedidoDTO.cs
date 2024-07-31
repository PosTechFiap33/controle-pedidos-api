using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("PagarPedido")]
    public class PagarPedidoDTO
    {
        [JsonPropertyName("codigoTransacao")]
        public string CodigoTransacao { get; set; }


        public PagarPedidoDTO(string codigoTransacao)
        {
            CodigoTransacao = codigoTransacao;
        }
    }

    [DisplayName("PagarPedidoManual")]
    public class PagarPedidoManualDTO : PagarPedidoDTO
    {
        [JsonPropertyName("pedidoId")]
        public Guid PedidoId { get; set; }

        [JsonPropertyName("valorPago")]
        public decimal ValorPago { get; set; }

        public PagarPedidoManualDTO(Guid pedidoId, string codigoTransacao, decimal valorPago) : base(codigoTransacao)
        {
            PedidoId = pedidoId;
            ValorPago = valorPago;
        }
    }
}

