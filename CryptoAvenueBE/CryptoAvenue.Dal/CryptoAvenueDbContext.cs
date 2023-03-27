using CryptoAvenue.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Dal
{
    public class CryptoAvenueDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<TradeOffer> TradeOffers { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        
        public CryptoAvenueDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=DESKTOP-DLVFJ7V\SQLEXPRESS;Database=CryptoAvenueDb;Trusted_Connection=True;", b => b.MigrationsAssembly("CryptoAvenue.Dal"))
                .EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
