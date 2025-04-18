using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameTracker.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToGameConsole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "GameConsoles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "GameConsoles");
        }
    }
}
