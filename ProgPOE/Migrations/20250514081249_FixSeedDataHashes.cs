using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgPOE.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedDataHashes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$12$EixZaYVK1fsbw1ZfbX3OXePaWxn96p36WQoeG6Lruj3vjPGga31lW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$12$cOPJCZJJE.2QbYxDF/ttBehGcXm8NmOSirvvJUCMD8u54hT988ZbW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$12$sNMUsmwqEAER4/ZbHKPPBuNtPrQXVOpY577eRCrDLkIqkyz6Qzt2C");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$12$UNIVUUdH7jG6Gq/vXudhrOoD5VrCzpkVmar1UNLStpl8Uus4iV8Eq");
        }
    }
}
