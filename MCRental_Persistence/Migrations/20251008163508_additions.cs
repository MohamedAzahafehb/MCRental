using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MCRental_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class additions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Steden",
                columns: new[] { "Id", "Naam", "Postcode" },
                values: new object[,]
                {
                    { 1, "Brussel", "1000" },
                    { 2, "Antwerpen", "2000" },
                    { 3, "Leuven", "3000" },
                    { 4, "Luik", "4000" },
                    { 5, "Namen", "5000" },
                    { 6, "Charleroi", "6000" },
                    { 7, "Bergen", "7000" },
                    { 8, "Brugge", "8000" },
                    { 9, "Gent", "9000" }
                });

            migrationBuilder.InsertData(
                table: "Filialen",
                columns: new[] { "Id", "Adres", "Email", "Naam", "StadId", "Telefoon" },
                values: new object[,]
                {
                    { 1, "Kerkstraat 1", "info.antwerpen@mcrental.be", "Filiaal Antwerpen", 2, "03 123 45 67" },
                    { 2, "Grote Markt 1", "info.brussel@mcrental.be", "Filiaal Brussel", 1, "02 123 45 67" },
                    { 3, "Bondgenotenlaan 1", "info.leuven@mcrental.be", "Filiaal Leuven", 3, "016 123 45 67" }
                });

            migrationBuilder.InsertData(
                table: "Klanten",
                columns: new[] { "Id", "Achternaam", "Adres", "Email", "GeboorteDatum", "StadId", "Telefoon", "Voornaam" },
                values: new object[,]
                {
                    { 1, "Janssens", "Kerkstraat 10", "jan.janssens@hotmail.com", new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "0470 123 456", "Jan" },
                    { 2, "Pieters", "Markt 5", "piet.pieters@hotmail.com", new DateTime(1990, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "0471 654 321", "Piet" },
                    { 3, "Klaassen", "Bondgenotenlaan 3", "klaas.klaassen@hotmail.com", new DateTime(1978, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "0472 789 012", "Klaas" }
                });

            migrationBuilder.InsertData(
                table: "Autos",
                columns: new[] { "Id", "Beschikbaar", "DagPrijs", "FiliaalId", "Merk", "Model", "Nummerplaat", "type" },
                values: new object[,]
                {
                    { 1, true, 70.0, 1, "Toyota", "Corolla", "1-ABC-123", "Sedan" },
                    { 2, true, 65.0, 2, "Ford", "Focus", "1-DEF-456", "Hatchback" },
                    { 3, false, 95.0, 3, "BMW", "X3", "1-GHI-789", "SUV" },
                    { 4, true, 90.0, 1, "Audi", "A4", "1-JKL-012", "Sedan" },
                    { 5, true, 75.0, 2, "Volkswagen", "Golf", "1-MNO-345", "Hatchback" },
                    { 6, false, 100.0, 3, "Mercedes", "GLC", "1-PQR-678", "SUV" }
                });

            migrationBuilder.InsertData(
                table: "Reservaties",
                columns: new[] { "Id", "AutoId", "EindDatum", "KlantId", "StartDatum" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 5, new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reservaties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservaties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservaties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Steden",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Steden",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Steden",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Steden",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Steden",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Steden",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Filialen",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Klanten",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Klanten",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Klanten",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Filialen",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Filialen",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Steden",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Steden",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Steden",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
