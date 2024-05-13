using ControlePedido.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlePedido.Infra.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.OwnsOne(c => c.Email, email =>
            {
                email.Property(c => c.Endereco)
                      .IsRequired()
                      .HasColumnName("Email");
            });

            builder.OwnsOne(c => c.Cpf, cpf =>
            {
                cpf.Property(c => c.Numero)
                   .IsRequired()
                   .HasColumnName("Cpf");

                cpf.HasIndex(c => c.Numero).IsUnique();
            });

            builder.ToTable("Clientes");
        }
    }
}

