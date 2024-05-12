using ControlePedido.Infra;
using ControlePedido.Infra.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ControlePedido.Api.Configuration
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseConfiguration(configuration);

            var connectionString = configuration["DbConnection"];
            services.AddDbContext<ControlePedidoContext>(options => options.UseNpgsql(connectionString));

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