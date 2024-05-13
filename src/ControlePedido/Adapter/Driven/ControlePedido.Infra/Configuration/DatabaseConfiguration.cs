using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControlePedido.Infra.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<ControlePedidoContext>(options => options.UseNpgsql(connectionString));
            services.RegisterService();


            return services;
        }

        public static void ConfigureMigrationDatabase(this IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<ControlePedidoContext>();
            dbContext.Database.Migrate();
        }
    }
}