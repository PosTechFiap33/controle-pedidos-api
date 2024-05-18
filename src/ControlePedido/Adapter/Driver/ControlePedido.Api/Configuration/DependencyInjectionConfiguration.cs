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
            services.AddTransient<ICriarProdutoUseCase, CriarProdutoUseCase>();
            services.AddTransient<IListarProdutoPorCategoriaUseCase, ListarProdutoPorCategoriaUseCase>();
            services.AddTransient<ICriarPedidoUseCase, CriarPedidoUseCase>();
        }
    }
}

