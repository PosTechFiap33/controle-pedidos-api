using System.Text.Json.Serialization;

namespace ControlePedido.Payment;

public class PagamentoRecebidoMercadoPagoDto
{
    [JsonPropertyName("id")]
    public long TransacaoId { get; set; }

    [JsonPropertyName("external_reference")]
    public string PedidoId { get; set; }

    [JsonPropertyName("date_approved")]
    public DateTime DataHoraPagamento { get; set; }
}
