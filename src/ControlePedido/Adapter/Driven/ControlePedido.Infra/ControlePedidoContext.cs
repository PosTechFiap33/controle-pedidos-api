using System.ComponentModel.DataAnnotations;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlePedido.Infra
{
    public class ControlePedidoContext : DbContext, IUnitOfWork
    {
        public ControlePedidoContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ControlePedidoContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            return sucesso;
        }
    }
}