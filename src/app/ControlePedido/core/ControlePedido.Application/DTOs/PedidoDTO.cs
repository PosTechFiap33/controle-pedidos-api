using System.ComponentModel;
using ControlePedido.CrossCutting;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Application.DTOs
{
    [DisplayName("Pedido")]
    public class PedidoDTO
    {
        public Guid Id { get; private set; }
        public decimal Valor { get; private set; }
        public string? CpfCliente { get; private set; }
        public IEnumerable<ProdutoDTO> Itens { get; private set; }
        public string Status { get; private set; }
        public string DataHora { get; private set; }

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

