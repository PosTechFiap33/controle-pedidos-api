using ControlePedido.Domain.Adapters.Services;
using ControlePedido.Payment.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ControlePedido.Payment.Configuration
{
	public static class DependencyInjectionConfiguration
	{
        public static IServiceCollection RegisterPaymentServices(this IServiceCollection services)
        {
            services.AddTransient<IPagamentoService, PagamentoMercadoPagoService>();
            return services;
        }
    }
}

