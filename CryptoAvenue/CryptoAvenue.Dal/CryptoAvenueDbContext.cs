using CryptoAvenue.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoAvenue.Application
{
    public class CryptoAvenueDbContext : DbContext
    {
        public DbSet<Coin> Coins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletCoin> WalletCoins { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public CryptoAvenueDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=DESKTOP-DLVFJ7V\SQLEXPRESS;Database=CryptoAvenueDb2;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;",
                b => b.MigrationsAssembly("CryptoAvenue.Dal"))
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
            .HasOne(t => t.SourceCoin)
            .WithMany()
            .HasForeignKey(t => t.SourceCoinId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.TargetCoin)
                .WithMany()
                .HasForeignKey(t => t.TargetCoinId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
