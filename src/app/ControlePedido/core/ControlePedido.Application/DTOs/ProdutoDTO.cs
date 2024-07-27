using System.ComponentModel;
using System.Text.Json.Serialization;
using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("Produto")]
    public class ProdutoDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("preco")]
        public decimal Preco { get; set; }

        [JsonPropertyName("categoria")]
        public Categoria Categoria { get; set; }

        [JsonPropertyName("imagem")]
        public string Imagem { get; set; }

        public ProdutoDTO()
        {
        }

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

