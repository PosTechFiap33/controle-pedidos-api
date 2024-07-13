using ControlePedido.Domain.Adapters.Providers;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Payment.Services
{
    public class PagamentoMercadoPagoProvider : IPagamentoProvider
	{
		public PagamentoMercadoPagoProvider()
		{
		}

        public string GerarQRCodePagamento(Pedido pedido)
        {
            return "Integracao FAKE prossiga pra rota de pagamento!";
        }

        public Task<bool> ValidarTransacao(string codigoTransacao)
        {
            return Task.FromResult(true);
        }
    }
}

