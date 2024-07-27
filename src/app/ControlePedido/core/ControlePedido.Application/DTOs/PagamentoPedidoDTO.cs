using System.Text.Json.Serialization;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Application;

public class PagamentoPedidoDTO
{
    [JsonPropertyName("codigoTransacao")]
    public string CodigoTransacao { get; set; }

    [JsonPropertyName("dataHoraPagamento")]
    public DateTime DataHoraPagamento { get; set; }

    [JsonPropertyName("valorPago")]
    public decimal ValorPago { get; set; }

    public PagamentoPedidoDTO()
    {
    }

    public PagamentoPedidoDTO(PedidoPagamento pagamento)
    {
        CodigoTransacao = pagamento.CodigoTransacao;
        DataHoraPagamento = pagamento.DataHoraPagamento;
        ValorPago = pagamento.ValorPago;
    }
}
