using ControlePedido.Domain.Entities;

namespace ControlePedido.Application;

public class PagamentoPedidoDTO
{
    public string CodigoTransacao { get; private set; }
    public DateTime DataHoraPagamento { get; private set; }
    public decimal ValorPago { get; private set; }

    public PagamentoPedidoDTO(PedidoPagamento pagamento)
    {
        CodigoTransacao = pagamento.CodigoTransacao;
        DataHoraPagamento = pagamento.DataHoraPagamento;
        ValorPago = pagamento.ValorPago;
    }
}
