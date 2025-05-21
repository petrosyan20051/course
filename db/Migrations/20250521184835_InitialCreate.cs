using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace db.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Forename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhoAdded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WhoChanged = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Forename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverLicenceSeries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverLicenceNmber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhoAdded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WhoChanged = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DropAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhoAdded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WhoChanged = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDriver = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationCode = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhoAdded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WhoChanged = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Forename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdDriver = table.Column<int>(type: "int", nullable: false),
                    IdVehicle = table.Column<int>(type: "int", nullable: false),
                    MovePrice = table.Column<int>(type: "int", nullable: false),
                    IdlePrice = table.Column<int>(type: "int", nullable: false),
                    WhoAdded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WhoChanged = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    TransportVehicleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rates_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rates_Vehicles_TransportVehicleId",
                        column: x => x.TransportVehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCustomer = table.Column<int>(type: "int", nullable: false),
                    IdRoute = table.Column<int>(type: "int", nullable: false),
                    IdRate = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    WhoAdded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WhoChanged = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    RateId = table.Column<int>(type: "int", nullable: true),
                    RouteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Rates_RateId",
                        column: x => x.RateId,
                        principalTable: "Rates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "Distance", "IdCustomer", "IdRate", "IdRoute", "Note", "RateId", "RouteId", "WhenAdded", "WhenChanged", "WhoAdded", "WhoChanged" },
                values: new object[] { 5, null, 550, 2, 2, 1, "", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RateId",
                table: "Orders",
                column: "RateId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RouteId",
                table: "Orders",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_DriverId",
                table: "Rates",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_TransportVehicleId",
                table: "Rates",
                column: "TransportVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_DriverId",
                table: "Vehicles",
                column: "DriverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
