using System.ComponentModel;
using System.Text.Json.Serialization;
using ControlePedido.CrossCutting;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("Pedido")]
    public class PedidoDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("valor")]
        public decimal Valor { get; set; }

        [JsonPropertyName("cpfCliente")]
        public string? CpfCliente { get; set; }

        [JsonPropertyName("itens")]
        public IEnumerable<ProdutoDTO> Itens { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("dataHora")]
        public string DataHora { get; set; }

        public PedidoDTO()
        {
        }

        public PedidoDTO(Pedido pedido)
        {
            Id = pedido.Id;
            Valor = pedido.Valor;
            CpfCliente = pedido.Cliente?.Cpf?.Numero ?? "CPF não fornecido";
            Itens = pedido.Itens.Select(item => new ProdutoDTO(item.Produto)).ToList();
            Status = pedido.RetornarStatusAtual().GetDescription();
            DataHora = pedido.RetornarDataHora();
        }
    }
}

