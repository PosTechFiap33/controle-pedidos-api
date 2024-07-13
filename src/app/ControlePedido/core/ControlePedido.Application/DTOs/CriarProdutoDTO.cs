using System.ComponentModel;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("CriarProduto")]
    public class CriarProdutoDTO
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public Categoria Categoria { get; set; }
        public string NomeImagem { get; set; }
        public string UrlImagem { get; set; }
        public string ExtensaoImagem { get; set; }
    }
}

