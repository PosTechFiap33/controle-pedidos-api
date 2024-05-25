using System.ComponentModel.DataAnnotations;

namespace ControlePedido.Application.DTOs
{
    public class CriarClienteDTO
    {
        [Required(ErrorMessage = "Campo {0} obrigatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatorio")]
        [EmailAddress(ErrorMessage = "Campo {0} invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatorio")]
        public string Cpf { get; set; }
    }
}

