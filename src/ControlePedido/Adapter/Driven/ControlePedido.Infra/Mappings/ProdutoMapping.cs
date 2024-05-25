using ControlePedido.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlePedido.Infra.Mappings;

public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Nome)
                  .IsRequired()
                  .HasMaxLength(100);

        builder.Property(p => p.Descricao)
               .HasMaxLength(500);

        builder.Property(p => p.Preco)
               .IsRequired();

        builder.Property(p => p.Categoria)
               .IsRequired();

        builder.OwnsOne(p => p.Imagem, imagem =>
        {
            imagem.Property(i => i.Nome)
                  .HasColumnName("NomeImagem")
                  .HasMaxLength(100);

            imagem.Property(i => i.Extensao)
                .HasColumnName("ExtensaoImagem")
                .HasMaxLength(10);

            imagem.Property(i => i.Url)
                        .HasColumnName("UrlImagem");
        });

        builder.ToTable("Produtos");
    }
}
