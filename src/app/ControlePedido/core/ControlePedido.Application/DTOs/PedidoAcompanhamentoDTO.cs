using System.ComponentModel;
using ControlePedido.CrossCutting;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Application.DTOs;

[DisplayName("AcompanhamentoPedido")]
public class AcompanhamentoPedidoDTO
{
    public Guid Id { get; private set; }
    public string? CpfCliente { get; private set; }
    public string Status { get; private set; }
    public decimal Valor { get; set; }
    public PagamentoPedidoDTO? DadosPagamento { get; set; }

    public AcompanhamentoPedidoDTO(Pedido pedido)
    {
        Id = pedido.Id;
        Valor = pedido.Valor;

        if (pedido.Cliente is not null)
            CpfCliente = pedido.Cliente.Cpf.Numero;
        else
            CpfCliente = "Cpf não informado!";

        if (pedido.Pagamento is not null)
            DadosPagamento = new PagamentoPedidoDTO(pedido.Pagamento);

        Status = pedido.RetornarStatusAtual().GetDescription();
    }
}
