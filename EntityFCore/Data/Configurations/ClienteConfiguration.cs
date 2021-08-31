using EntityFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFCore.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(e => e.Telefone).HasColumnType("CHAR(11)");
            builder.Property(e => e.CEP).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(e => e.Estado).HasColumnType("CHAR(2)").IsRequired();
            builder.Property(e => e.Cidade).HasMaxLength(60).IsRequired();

            builder.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
        }
    }
}
