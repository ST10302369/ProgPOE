using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgPOE.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Energies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnergySource = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnergyUsage = table.Column<double>(type: "float", nullable: false),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Energies", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Energies");
        }
    }
}
