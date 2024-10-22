using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IWork.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdvertisementStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "NormalAdvertisement",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DynamicAdvertisement",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "NormalAdvertisement");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DynamicAdvertisement");
        }
    }
}
