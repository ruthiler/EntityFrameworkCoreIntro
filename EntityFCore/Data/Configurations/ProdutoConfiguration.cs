using EntityFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFCore.Data.Configurations
{
    class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
            builder.Property(e => e.Descricao).HasColumnType("VARCHAR(60)");
            builder.Property(e => e.Valor).IsRequired();
            builder.Property(e => e.TipoProduto).HasConversion<string>();
        }
    }
}
