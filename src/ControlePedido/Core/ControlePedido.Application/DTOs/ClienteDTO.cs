using System.ComponentModel;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("Cliente")]
    public class ClienteDTO
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }

        public ClienteDTO(string nome, string cpf, string email)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }
    }
}

