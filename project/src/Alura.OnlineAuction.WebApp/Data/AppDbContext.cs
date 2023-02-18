using Alura.OnlineAuctions.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Alura.OnlineAuctions.WebApp.Data
{
    public sealed class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=AluraAuctionsDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Leilao>()
                .HasOne(l => l.Categoria)
                .WithMany(c => c.Leiloes)
                .HasForeignKey(l => l.IdCategoria);
        }

        public DbSet<Leilao> Leiloes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}