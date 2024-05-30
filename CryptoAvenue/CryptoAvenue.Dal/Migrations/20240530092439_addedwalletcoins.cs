using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoAvenue.Dal.Migrations
{
    /// <inheritdoc />
    public partial class addedwalletcoins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WalletCoins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoinId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletCoins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletCoins_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WalletCoins_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WalletCoins_CoinId",
                table: "WalletCoins",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletCoins_WalletId",
                table: "WalletCoins",
                column: "WalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalletCoins");
        }
    }
}
