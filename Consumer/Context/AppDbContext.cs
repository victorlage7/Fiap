using Consumer.Entity;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() {}

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).HasColumnType("varchar(100)");
                entity.Property(e => e.Email).HasColumnType("varchar(100)");
                entity.Property(e => e.DataCadastro);
                entity.Property(e => e.DataProcessamento);
            });
        }
    }
}