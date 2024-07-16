using ControlePedido.Domain.Adapters.Providers;
using ControlePedido.Payment.Services;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace ControlePedido.Payment.Configurations;

public static class RefitConfiguration
{
    public static IServiceCollection ConfigureHttpPayment(this IServiceCollection services)
    {
        services.AddRefitClient<MercadoPagoApi>()
               .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.mercadopago.com"));
        services.AddTransient<IPagamentoProvider, PagamentoMercadoPagoProvider>();
        return services;
    }
}
