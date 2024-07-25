using ControlePedido.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlePedido.Infra.Mappings;

public class PedidoPagamentoMapping : IEntityTypeConfiguration<PedidoPagamento>
{
    public void Configure(EntityTypeBuilder<PedidoPagamento> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CodigoTransacao)
               .IsRequired();

        builder.Property(p => p.DataHoraPagamento)
               .IsRequired();

        builder.Property(p => p.ValorPago)
               .IsRequired();

        builder.ToTable("Pagamentos");
    }
}

public class PedidoStatusMapping : IEntityTypeConfiguration<PedidoStatus>
{
    public void Configure(EntityTypeBuilder<PedidoStatus> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.DataHora)
               .IsRequired();

        builder.Property(p => p.Status)
               .IsRequired();

        builder.HasOne(p => p.Pedido)
               .WithMany(p => p.Status)
               .HasForeignKey(p => p.PedidoId);

        builder.ToTable("PedidoStatus");
    }
}

public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Produto)
               .WithMany(p => p.ItensPedido)
               .HasForeignKey(p => p.ProdutoId);

        builder.HasOne(p => p.Pedido)
               .WithMany(p => p.Itens)
               .HasForeignKey(p => p.PedidoId);

        builder.ToTable("PedidoItens");
    }
}

public class PedidoMapping : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Valor)
               .IsRequired();

        builder.Property(p => p.ClienteId)
               .IsRequired(false);

        builder.HasOne(p => p.Cliente)
               .WithMany(p => p.Pedidos)
               .HasForeignKey(p => p.ClienteId);

        builder.HasMany(p => p.Itens)
               .WithOne(p => p.Pedido)
               .HasForeignKey(p => p.PedidoId);

        builder.HasMany(p => p.Status)
               .WithOne(p => p.Pedido)
               .HasForeignKey(p => p.PedidoId);

        builder.HasOne(p => p.Pagamento)
               .WithOne(p => p.Pedido)
               .HasForeignKey<PedidoPagamento>(p => p.PedidoId);

        builder.ToTable("Pedidos");

    }
}
