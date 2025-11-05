using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCRental_Models.Migrations
{
    /// <inheritdoc />
    public partial class Res_dtAnnulatie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Aanmaking",
                table: "Reservaties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Annulatie",
                table: "Reservaties",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aanmaking",
                table: "Reservaties");

            migrationBuilder.DropColumn(
                name: "Annulatie",
                table: "Reservaties");
        }
    }
}
