using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCRental_Models.Migrations
{
    /// <inheritdoc />
    public partial class Gebruikers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Naam",
                table: "AspNetUsers",
                newName: "Voornaam");

            migrationBuilder.AddColumn<string>(
                name: "GebruikerId",
                table: "Reservaties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Achternaam",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "GeboorteDatum",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StadId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Reservaties",
                keyColumn: "Id",
                keyValue: 1,
                column: "GebruikerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reservaties",
                keyColumn: "Id",
                keyValue: 2,
                column: "GebruikerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reservaties",
                keyColumn: "Id",
                keyValue: 3,
                column: "GebruikerId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Reservaties_GebruikerId",
                table: "Reservaties",
                column: "GebruikerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StadId",
                table: "AspNetUsers",
                column: "StadId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Steden_StadId",
                table: "AspNetUsers",
                column: "StadId",
                principalTable: "Steden",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservaties_AspNetUsers_GebruikerId",
                table: "Reservaties",
                column: "GebruikerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Steden_StadId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservaties_AspNetUsers_GebruikerId",
                table: "Reservaties");

            migrationBuilder.DropIndex(
                name: "IX_Reservaties_GebruikerId",
                table: "Reservaties");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StadId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GebruikerId",
                table: "Reservaties");

            migrationBuilder.DropColumn(
                name: "Achternaam",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Adres",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GeboorteDatum",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StadId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Voornaam",
                table: "AspNetUsers",
                newName: "Naam");
        }
    }
}
