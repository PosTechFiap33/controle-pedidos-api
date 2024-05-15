using ControlePedido.Api.Middleware;
using Microsoft.OpenApi.Models;

namespace ControlePedido.Api.Configuration
{
    public static class SwaggerConfiguration
    {

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });
                c.SchemaFilter<EnumDescriptionFilter>();
            });

            return services;
        }

        public static WebApplication UseSwaggerApp(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}

