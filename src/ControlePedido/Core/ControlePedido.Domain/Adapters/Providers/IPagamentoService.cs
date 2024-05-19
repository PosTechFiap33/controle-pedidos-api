using System.Threading.Tasks;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Domain.Adapters.Providers
{
    public interface IPagamentoProvider
	{
		string GerarQRCodePagamento(Pedido pedido);
        Task<bool> ValidarTransacao(string codigoTransacao);
    }
}

