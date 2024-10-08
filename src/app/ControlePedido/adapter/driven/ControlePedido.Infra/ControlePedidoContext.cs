﻿using System.ComponentModel.DataAnnotations;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlePedido.Infra
{
    public class ControlePedidoContext : DbContext, IUnitOfWork
    {
        public ControlePedidoContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoPagamento> Pagamento { get; set; }
        public DbSet<PedidoStatus> PedidoStatus { get; set; }

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