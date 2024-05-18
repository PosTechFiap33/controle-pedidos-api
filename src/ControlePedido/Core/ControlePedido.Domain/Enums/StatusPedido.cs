using System.ComponentModel;

namespace ControlePedido.Domain.Enums
{
    public enum StatusPedido
	{

		[Description("Recebido")]
		RECEBIDO = 1,

		[Description("Em preparacao")]
		EM_PREPARACAO = 2,

        [Description("Pronto")]
        PRONTO = 3,

        [Description("Finalizado")]
		FINALIZADO = 4

	}
}

