using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ControlePedido.Infra.Configuration
{
    public static class DependencyInjectionConfiguration
    {

        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddScoped<ControlePedidoContext>();
            services.AddTransient<IClienteRepository, ClienteRepository>();

            return services;
        }

    }
}

