using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoAvenue.Dal.Migrations
{
    /// <inheritdoc />
    public partial class addedTradeOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SentCoinId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivedCoinId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SentAmount = table.Column<double>(type: "float", nullable: false),
                    ReceivedAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeOffers_Coins_ReceivedCoinId",
                        column: x => x.ReceivedCoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeOffers_Coins_SentCoinId",
                        column: x => x.SentCoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeOffers_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeOffers_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeOffers_ReceivedCoinId",
                table: "TradeOffers",
                column: "ReceivedCoinId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeOffers_RecipientId",
                table: "TradeOffers",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeOffers_SenderId",
                table: "TradeOffers",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeOffers_SentCoinId",
                table: "TradeOffers",
                column: "SentCoinId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeOffers");
        }
    }
}
