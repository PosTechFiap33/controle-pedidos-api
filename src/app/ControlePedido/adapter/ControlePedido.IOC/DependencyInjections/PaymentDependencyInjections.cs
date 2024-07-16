using ControlePedido.Payment.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace ControlePedido.IOC.DependencyInjections
{
    public static class PaymentDependencyInjections
    {
        public static IServiceCollection RegisterPaymentServices(this IServiceCollection services)
        {
            services.ConfigureHttpPayment();
            return services;
        }
    }
}

