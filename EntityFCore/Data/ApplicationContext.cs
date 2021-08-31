using System.Linq;
using EntityFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFCore.Data
{
    public class ApplicationContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

        public DbSet<Pedido> Pedidos { get; set; }
        
        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer($"Application Name=Curso_EF; Server=localhost; Database=CursoEF; User Id=sa; Password=abcd.1234;",
                p => p.EnableRetryOnFailure());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeia todas as classes de uma só vez, sem precisar especificar 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            // MapearPropriedadesEsquecidas(modelBuilder);

            // Mapeia de acordo com as classes de configurações
            // modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            // modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            // modelBuilder.ApplyConfiguration(new PedidoItemConfiguration());
            // modelBuilder.ApplyConfiguration(new ProdutoConfiguration());

            // Não mapear todas as entidades no mesmo arquivos
        }

        private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where( p => p.ClrType == typeof(string));

                foreach(var property in properties)
                {
                    if(string.IsNullOrEmpty(property.GetColumnType())
                        && !property.GetMaxLength().HasValue)
                    {
                        //property.SetMaxLength(100);
                        property.SetColumnType("NVARCHAR(100)");
                    }

                }
            }
        }
    }
}
