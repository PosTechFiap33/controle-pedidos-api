using System.Text.Json.Serialization;
using ControlePedido.Api.Middleware;
using ControlePedido.IOC.DependencyInjections;
using ControlePedido.Infra.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedido.Api.Configuration
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(options =>
                options.Filters.Add<CustomModelStateValidationFilter>()
            ).AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddDatabaseConfiguration(configuration);

            services.RegisterRepositories();

            services.RegisterPaymentServices();

            services.RegisterServices();

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