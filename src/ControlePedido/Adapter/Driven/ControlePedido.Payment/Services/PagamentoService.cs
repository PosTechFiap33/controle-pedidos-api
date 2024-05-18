using ControlePedido.Domain.Adapters.Services;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Payment.Services
{
    public class PagamentoMercadoPagoService : IPagamentoService
	{
		public PagamentoMercadoPagoService()
		{
		}

        public string GerarQRCodePagamento(Pedido pedido)
        {
            throw new NotImplementedException();
        }
    }
}

