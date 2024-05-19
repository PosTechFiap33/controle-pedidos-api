using ControlePedido.Domain.Adapters.Providers;
using ControlePedido.Payment.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ControlePedido.Payment.Configuration
{
    public static class DependencyInjectionConfiguration
	{
        public static IServiceCollection RegisterPaymentServices(this IServiceCollection services)
        {
            services.AddTransient<IPagamentoProvider, PagamentoMercadoPagoProvider>();
            return services;
        }
    }
}

