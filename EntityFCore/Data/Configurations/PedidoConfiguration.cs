using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFCore.Data.Configurations
{
    class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(e => e.StatusPedido).HasConversion<string>();
            builder.Property(e => e.TipoFrete).HasConversion<int>();
            builder.Property(e => e.Observacao).HasColumnType("VARCHAR(512)");

            builder.HasMany(e => e.Itens)
                .WithOne(e => e.Pedido)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
