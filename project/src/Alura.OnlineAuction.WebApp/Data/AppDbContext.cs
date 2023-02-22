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
            modelBuilder.Entity<Auction>()
                .HasOne(l => l.Category)
                .WithMany(c => c.Auctions)
                .HasForeignKey(l => l.IdCategory);
        }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}