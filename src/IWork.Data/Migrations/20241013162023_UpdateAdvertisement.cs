using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IWork.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdvertisement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "NormalAdvertisement");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "itemAdvertisement");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "NormalAdvertisement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "itemAdvertisement",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
