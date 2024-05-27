using ControlePedido.Domain.Adapters.Providers;
using ControlePedido.Payment.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ControlePedido.IOC.DependencyInjections
{
    public static class PaymentDependencyInjections
    {
        public static IServiceCollection RegisterPaymentServices(this IServiceCollection services)
        {
            services.AddTransient<IPagamentoProvider, PagamentoMercadoPagoProvider>();
            return services;
        }
    }
}

