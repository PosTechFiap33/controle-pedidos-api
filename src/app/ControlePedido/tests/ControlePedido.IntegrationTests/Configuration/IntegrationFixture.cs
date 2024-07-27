using ControlePedido.Domain.Entities;
using ControlePedido.Domain.Enums;
using ControlePedido.Domain.ValueObjects;
using ControlePedido.Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
        db.Cliente.Add(new Cliente("Teste", "71935710010", "teste@testecadastrado.com"));

        if (!db.Produto.Any(e => e.Id == new Guid("d0c1c104-4b17-4b24-8195-94d9b1a10b0b")))
            db.Produto.Add(new Produto(new Guid("d0c1c104-4b17-4b24-8195-94d9b1a10b0b"), "Hamburguer", 30m, Categoria.Lanche, "Delicioso Hamburguer artesanal.", new Imagem("http://img.com/hamburguer", "jpg", "hamburguer")));

        if (!db.Produto.Any(e => e.Id == new Guid("1d93f2c3-f3a7-4e1e-b0d6-3568d2e96e43")))
            db.Produto.Add(new Produto(new Guid("1d93f2c3-f3a7-4e1e-b0d6-3568d2e96e43"), "Sunday", 20m, Categoria.Sobremesa, "Deliciosa sobremesa", new Imagem("http://img.com/sunday", "jpg", "sunday")));

        await db.SaveChangesAsync();
    }
}

