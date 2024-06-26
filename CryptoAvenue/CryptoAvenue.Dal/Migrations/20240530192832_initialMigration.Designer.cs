﻿// <auto-generated />
using System;
using CryptoAvenue.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CryptoAvenue.Dal.Migrations
{
    [DbContext(typeof(CryptoAvenueDbContext))]
    [Migration("20240530192832_initialMigration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CryptoAvenue.Domain.Models.Coin", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Ath")
                        .HasColumnType("float");

                    b.Property<double>("CurrentPrice")
                        .HasColumnType("float");

                    b.Property<double>("High24h")
                        .HasColumnType("float");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Low24h")
                        .HasColumnType("float");

                    b.Property<double>("MarketCap")
                        .HasColumnType("float");

                    b.Property<double>("MarketCapChange24h")
                        .HasColumnType("float");

                    b.Property<double>("MarketCapChangePercentage24h")
                        .HasColumnType("float");

                    b.Property<int>("MarketCapRank")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PriceChange24h")
                        .HasColumnType("float");

                    b.Property<double>("PriceChangePercentage24h")
                        .HasColumnType("float");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Coins");
                });

            modelBuilder.Entity("CryptoAvenue.Domain.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SourceCoinId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("SourcePrice")
                        .HasColumnType("float");

                    b.Property<double>("SourceQuantity")
                        .HasColumnType("float");

                    b.Property<string>("TargetCoinId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("TargetPrice")
                        .HasColumnType("float");

                    b.Property<double>("TargetQuantity")
                        .HasColumnType("float");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SourceCoinId");

                    b.HasIndex("TargetCoinId");

                    b.HasIndex("WalletId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CryptoAvenue.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CryptoAvenue.Domain.Models.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("CryptoAvenue.Domain.Models.WalletCoin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CoinId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CoinId");

                    b.HasIndex("WalletId");

                    b.ToTable("WalletCoins");
                });

            modelBuilder.Entity("CryptoAvenue.Domain.Models.Transaction", b =>
                {
                    b.HasOne("CryptoAvenue.Domain.Models.Coin", "SourceCoin")
                        .WithMany()
                        .HasForeignKey("SourceCoinId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CryptoAvenue.Domain.Models.Coin", "TargetCoin")
                        .WithMany()
                        .HasForeignKey("TargetCoinId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CryptoAvenue.Domain.Models.Wallet", "Wallet")
                        .WithMany("Transactions")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SourceCoin");

                    b.Navigation("TargetCoin");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("CryptoAvenue.Domain.Models.Wallet", b =>
                {
                    b.HasOne("CryptoAvenue.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoAvenue.Domain.Models.WalletCoin", b =>
                {
                    b.HasOne("CryptoAvenue.Domain.Models.Coin", "Coin")
                        .WithMany()
                        .HasForeignKey("CoinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CryptoAvenue.Domain.Models.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coin");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("CryptoAvenue.Domain.Models.Wallet", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
