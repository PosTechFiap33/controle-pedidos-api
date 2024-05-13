using ControlePedido.Infra.Configuration;

namespace ControlePedido.Api.Configuration
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseConfiguration(configuration);

            services.RegisterServices();

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddCors(option =>
            {
                option.AddPolicy("Total",
                    builder =>
                      builder.AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader()
                    );
            });

            return services;
        }
    }
}