using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("ItemPedido")]
    public class PedidoItemDTO
    {
        [Required(ErrorMessage = "Campo {0} obrigatorio")]
        public Guid ProdutoId { get; set; }
    }

    [DisplayName("CriarPedido")]
    public class CriarPedidoDTO
    {
        public string? CpfCliente { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatorio")]
        public ICollection<PedidoItemDTO> Itens { get; set; }

        public CriarPedidoDTO()
        {
            Itens = new List<PedidoItemDTO>();
        }
    }
}

