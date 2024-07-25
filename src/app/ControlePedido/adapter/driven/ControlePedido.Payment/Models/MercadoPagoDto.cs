namespace ControlePedido.Payment;

using System.Collections.Generic;
using System.Text.Json.Serialization;
using ControlePedido.Domain.Entities;

public class SaqueMercadoPagoDto
{
    [JsonPropertyName("amount")]
    public decimal Quantia { get; set; }

    public SaqueMercadoPagoDto()
    {
        Quantia = 0;
    }
}

public class ItemMercadoPagoDto
{
  
    [JsonPropertyName("title")]
    public string Titulo { get; set; }

    [JsonPropertyName("description")]
    public string Descricao { get; set; }

    [JsonPropertyName("unit_price")]
    public decimal PrecoUnitario { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantidade { get; set; }

    [JsonPropertyName("unit_measure")]
    public string UnidadeMedida { get; set; }

    [JsonPropertyName("total_amount")]
    public decimal QuantiaTotal { get; set; }
}

public class IntegradorMercadoPagoDto
{
    [JsonPropertyName("id")]
    public long UserId { get; set; }
}

public class PedidoMercadoPagoDto
{
    [JsonPropertyName("cash_out")]
    public SaqueMercadoPagoDto Saque { get; set; }

    [JsonPropertyName("description")]
    public string Descricao { get; set; }

    [JsonPropertyName("external_reference")]
    public string ReferenciaExterna { get; set; }

    [JsonPropertyName("items")]
    public List<ItemMercadoPagoDto> Itens { get; set; }

    [JsonPropertyName("notification_url")]
    public string UrlNotificacao { get; set; }

    [JsonPropertyName("sponsor")]
    public IntegradorMercadoPagoDto Patrocinador { get; set; }

    [JsonPropertyName("title")]
    public string Titulo { get; set; }

    [JsonPropertyName("total_amount")]
    public decimal QuantiaTotal { get; set; }

    public PedidoMercadoPagoDto(Pedido pedido, string urlNotificacao)
    {
        Saque = new SaqueMercadoPagoDto();
        Descricao = $"Pedido número {pedido.Id}";
        ReferenciaExterna = pedido.Id.ToString();
        Itens = pedido.Itens.Select(i => new ItemMercadoPagoDto{
            Titulo = i.Produto.Nome,
            Descricao = i.Produto.Descricao,
            PrecoUnitario = i.Produto.Preco,
            Quantidade = 1,
            UnidadeMedida = "unit",
            QuantiaTotal = i.Produto.Preco
        }).ToList();
        UrlNotificacao = urlNotificacao;
        Titulo = "Pagamento para controle de pedido";
        QuantiaTotal = pedido.Valor;
    }
}

public class QrCodeMercadoPagoDto
{
    [JsonPropertyName("in_store_order_id")]
    public string InStoreOrderId { get; set; }

    [JsonPropertyName("qr_data")]
    public string QrData { get; set; }
}
