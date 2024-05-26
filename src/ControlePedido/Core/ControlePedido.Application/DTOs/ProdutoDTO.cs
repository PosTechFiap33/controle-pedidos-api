using System.ComponentModel;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("Produto")]
    public class ProdutoDTO
	{
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public Categoria Categoria { get; private set; }
        public string Imagem { get; private set; }

        public ProdutoDTO(Produto produto)
        {
            Id = produto.Id;
            Nome = produto.Nome;
            Descricao = produto.Descricao;
            Preco = produto.Preco;
            Categoria = produto.Categoria;
            Imagem = produto.Imagem.UrlExibicao;
        }
    }
}

