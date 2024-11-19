using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IWork.Data.Migrations
{
    /// <inheritdoc />
    public partial class HiringAdvertisement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PreferenceId",
                table: "HiringAdvertisements",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PreferenceId",
                table: "HiringAdvertisements",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60);
        }
    }
}
