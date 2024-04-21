using CryptoAvenue.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application
{
    public class CryptoAvenueDbContext : DbContext
    {
        public DbSet<Coin> Coins { get; set; }
        public DbSet<User> Users { get; set; }
        public CryptoAvenueDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=DESKTOP-DLVFJ7V\SQLEXPRESS;Database=CryptoAvenueDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;",
                b => b.MigrationsAssembly("CryptoAvenue.Dal"))
                .EnableSensitiveDataLogging();
        }
    }
}
