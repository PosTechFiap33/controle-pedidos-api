using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Infra;
using ControlePedido.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ControlePedido.IOC.DependencyInjections
{
    public static class RepositoryDependencyInjections
	{
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ControlePedidoContext>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();

            return services;
        }
    }
}

