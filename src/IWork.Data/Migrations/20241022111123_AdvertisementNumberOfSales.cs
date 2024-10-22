using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IWork.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdvertisementNumberOfSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfSales",
                table: "NormalAdvertisement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSales",
                table: "DynamicAdvertisement",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfSales",
                table: "NormalAdvertisement");

            migrationBuilder.DropColumn(
                name: "NumberOfSales",
                table: "DynamicAdvertisement");
        }
    }
}
