using System.ComponentModel;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("PagarPedido")]
    public class PagarPedidoDTO
	{
		public Guid PedidoId { get; set; }
		public string CodigoTransacao { get; set; }
	}
}

