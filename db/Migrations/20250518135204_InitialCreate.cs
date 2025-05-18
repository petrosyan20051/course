using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace db.Migrations {
    /// <inheritdoc />
    public partial class InitialCreate : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new {
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
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
