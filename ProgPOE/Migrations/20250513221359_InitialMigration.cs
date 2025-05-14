using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProgPOE.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Energies");

            migrationBuilder.CreateTable(
                name: "Farmers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmerId = table.Column<int>(type: "int", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Farmers",
                columns: new[] { "Id", "Email", "FarmLocation", "FarmName", "FirstName", "LastName", "PhoneNumber", "RegistrationDate" },
                values: new object[,]
                {
                    { 1, "john.smith@example.com", "Eastern Cape", "Green Acres Farm", "John", "Smith", "0123456789", new DateTime(2025, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "mary.johnson@example.com", "Western Cape", "Sunrise Valley Farm", "Mary", "Johnson", "0987654321", new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FarmerId", "LastLogin", "Password", "RegistrationDate", "Role", "Username" },
                values: new object[] { 1, null, null, "employee123", new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Employee", "employee" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "FarmerId", "Name", "PricePerUnit", "ProductionDate", "Quantity", "Unit" },
                values: new object[,]
                {
                    { 1, "Vegetables", "Fresh organic tomatoes", 1, "Organic Tomatoes", 25.50m, new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 100.0, "kg" },
                    { 2, "Dairy & Eggs", "Farm-fresh free-range eggs", 1, "Free-range Eggs", 45.00m, new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 500.0, "dozen" },
                    { 3, "Vegetables", "Fresh sweet corn", 2, "Sweet Corn", 15.75m, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 200.0, "kg" },
                    { 4, "Fruits", "Freshly picked organic apples", 2, "Organic Apples", 18.50m, new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 300.0, "kg" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FarmerId", "LastLogin", "Password", "RegistrationDate", "Role", "Username" },
                values: new object[] { 2, 1, null, "farmer123", new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Farmer", "farmer" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_FarmerId",
                table: "Products",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FarmerId",
                table: "Users",
                column: "FarmerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Farmers");

            migrationBuilder.CreateTable(
                name: "Energies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnergySource = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnergyUsage = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Energies", x => x.Id);
                });
        }
    }
}
