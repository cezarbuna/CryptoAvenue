using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoAvenue.Dal.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyProperty",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPrice = table.Column<double>(type: "float", nullable: false),
                    MarketCap = table.Column<double>(type: "float", nullable: false),
                    MarketCapRank = table.Column<int>(type: "int", nullable: false),
                    High24h = table.Column<double>(type: "float", nullable: false),
                    Low24h = table.Column<double>(type: "float", nullable: false),
                    PriceChange24h = table.Column<double>(type: "float", nullable: false),
                    PriceChangePercentage24h = table.Column<double>(type: "float", nullable: false),
                    MarketCapChange24h = table.Column<double>(type: "float", nullable: false),
                    MarketCapChangePercentage24h = table.Column<double>(type: "float", nullable: false),
                    Ath = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyProperty", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyProperty");
        }
    }
}
