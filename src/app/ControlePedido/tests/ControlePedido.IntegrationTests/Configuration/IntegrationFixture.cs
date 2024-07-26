using ControlePedido.Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ControlePedido.IntegrationTests;

public class IntegrationTestFixture : IDisposable
{
    public WebApplicationFactory<Program> Factory { get; }
    public HttpClient Client { get; }

    public IntegrationTestFixture()
    {
        Factory = new WebApplicationFactory<Program>()
         .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                   {
                       context.HostingEnvironment.EnvironmentName = "Testing";
                   });

                builder.ConfigureServices(async services =>
                {
                    // Remove o contexto de banco de dados existente, se houver
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ControlePedidoContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<ControlePedidoContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    var serviceProvider = services.BuildServiceProvider();
                    using (var scope = serviceProvider.CreateScope())
                    {
                        {
                            var scopedServices = scope.ServiceProvider;
                            var db = scopedServices.GetRequiredService<ControlePedidoContext>();
                            db.Database.EnsureCreated();
                            await SeedDatabase(db);
                        }
                    }
                });
            });

        Client = Factory.CreateClient();
    }

    public void Dispose()
    {
        Client.Dispose();
        Factory.Dispose();
    }

    private async Task SeedDatabase(ControlePedidoContext db)
    {
        db.Cliente.Add(new Domain.Entities.Cliente("Teste", "71935710010", "teste@testecadastrado.com"));
        await db.SaveChangesAsync();
    }
}

