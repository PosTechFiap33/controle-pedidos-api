using ControlePedido.Domain.Entities;

namespace ControlePedido.Domain.Adapters.Services
{
    public interface IPagamentoService
	{
		string GerarQRCodePagamento(Pedido pedido);
	}
}

