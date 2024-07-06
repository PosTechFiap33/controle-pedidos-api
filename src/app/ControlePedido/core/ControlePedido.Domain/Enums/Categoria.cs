using System.ComponentModel;

namespace ControlePedido.Domain.Enums
{
    public enum Categoria
    {
        [Description("Lanche")]
        Lanche = 1,

        [Description("Acompanhamento")]
        Acompanhamento = 2,

        [Description("Bebida")]
        Bebida = 3,

        [Description("Sobremesa")]
        Sobremesa = 4
    }
}
