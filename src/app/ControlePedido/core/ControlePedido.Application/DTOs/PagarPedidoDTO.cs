using System.ComponentModel;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("PagarPedido")]
    public class PagarPedidoDTO
	{
        public string CodigoTransacao { get; private set; }
        
        public PagarPedidoDTO(string codigoTransacao)
        {
            CodigoTransacao = codigoTransacao;
        }
	}
}

