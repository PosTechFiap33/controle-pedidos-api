using ControlePedido.Application.UseCases.Clientes;
using ControlePedido.Application.UseCases.Pedidos;
using ControlePedido.Application.UseCases.Produtos;

namespace ControlePedido.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ICriarClienteUseCase, CriarClienteUseCase>();
            services.AddTransient<IListarTodosClientesUseCase, ListarTodosClientesUseCase>();
            services.AddTransient<ICriarProdutoUseCase, CriarProdutoUseCase>();
            services.AddTransient<IListarProdutoPorCategoriaUseCase, ListarProdutoPorCategoriaUseCase>();
            services.AddTransient<ICriarPedidoUseCase, CriarPedidoUseCase>();
            services.AddTransient<IPagarPedidoUseCase, PagarPedidoUseCase>();
            services.AddTransient<IListarPedidoPorStatusUseCase, ListarPedidoPorStatusUseCase>();
            services.AddTransient<IIniciarPreparoPedidoUseCase, IniciarPreparoPedidoUseCase>();
            services.AddTransient<IFinalizarPreparoPedidoUseCase, FinalizarPreparoPedidoUseCase>();
            services.AddTransient<IEntregarPedidoUseCase, EntregarPedidoUseCase>();
        }
    }
}

