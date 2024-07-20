using ControlePedido.Application.UseCases.Clientes;
using ControlePedido.Application.UseCases.Pedidos;
using ControlePedido.Application.UseCases.Produtos;
using Microsoft.Extensions.DependencyInjection;

namespace ControlePedido.IOC.DependencyInjections
{
    public static class UseCaseDependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ICriarClienteUseCase, CriarClienteUseCase>();
            services.AddTransient<IListarTodosClientesUseCase, ListarTodosClientesUseCase>();
            services.AddTransient<ICriarProdutoUseCase, CriarProdutoUseCase>();
            services.AddTransient<IListarProdutoPorCategoriaUseCase, ListarProdutoPorCategoriaUseCase>();
            services.AddTransient<ICriarPedidoUseCase, CriarPedidoUseCase>();
            services.AddTransient<IPagarPedidoUseCase, PagarPedidoUseCase>();
            services.AddTransient<IPagarPedidoManualmenteUseCase, PagarPedidoManualmenteUseCase>();
            services.AddTransient<IListarPedidoUseCase, ListarPedidoUseCase>();
            services.AddTransient<IIniciarPreparoPedidoUseCase, IniciarPreparoPedidoUseCase>();
            services.AddTransient<IFinalizarPreparoPedidoUseCase, FinalizarPreparoPedidoUseCase>();
            services.AddTransient<IEntregarPedidoUseCase, EntregarPedidoUseCase>();
            services.AddTransient<IAcompanharPedidoUseCase, AcompanharPedidoUseCase>();
        }
    }
}

