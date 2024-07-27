using System.ComponentModel;
using System.Text.Json.Serialization;
using ControlePedido.CrossCutting;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Application.DTOs;

[DisplayName("AcompanhamentoPedido")]
public class AcompanhamentoPedidoDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("cpfCliente")]
    public string? CpfCliente { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("valor")]
    public decimal Valor { get; set; }

    [JsonPropertyName("dadosPagamento")]
    public PagamentoPedidoDTO? DadosPagamento { get; set; }

    public AcompanhamentoPedidoDTO()
    {
    }

    public AcompanhamentoPedidoDTO(Pedido pedido)
    {
        Id = pedido.Id;
        Valor = pedido.Valor;

        if (pedido.Cliente is not null)
            CpfCliente = pedido.Cliente.Cpf.Numero;
        else
            CpfCliente = "CPF não fornecido";

        if (pedido.Pagamento is not null)
            DadosPagamento = new PagamentoPedidoDTO(pedido.Pagamento);

        Status = pedido.RetornarStatusAtual().GetDescription();
    }
}
