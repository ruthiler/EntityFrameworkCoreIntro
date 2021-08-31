using EntityFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFCore.Data.Configurations
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItens");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Quantidade).HasDefaultValue(1).IsRequired();
            builder.Property(e => e.Valor).IsRequired();
            builder.Property(e => e.Desconto).IsRequired();
        }
    }
}