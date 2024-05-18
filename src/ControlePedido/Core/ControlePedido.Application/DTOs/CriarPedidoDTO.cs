using System.ComponentModel.DataAnnotations;

namespace ControlePedido.Application.DTOs
{
    public class PedidoItemDTO
    {
        [Required(ErrorMessage = "Campo {0} obrigatorio")]
        public Guid ProdutoId { get; set; }
    }

    public class CriarPedidoDTO
    {
        public Guid? ClienteId { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatorio")]
        public ICollection<PedidoItemDTO> Itens { get; set; }
    }
}

