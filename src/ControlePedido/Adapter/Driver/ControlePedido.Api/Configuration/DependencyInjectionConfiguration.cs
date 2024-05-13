using ControlePedido.Application.UseCases.Clientes;

namespace ControlePedido.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ICriarClienteUseCase, CriarClienteUseCase>();
        }
    }
}

