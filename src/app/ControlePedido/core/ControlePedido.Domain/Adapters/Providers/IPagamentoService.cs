using System.Threading.Tasks;
using ControlePedido.Domain.Adapters.DTOs;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Domain.Adapters.Providers
{
    public interface IPagamentoProvider
	{
		Task<string> GerarQRCodePagamento(Pedido pedido);
        Task<PagamentoRealizadoDTO> ValidarTransacao(string codigoTransacao);
    }
}

