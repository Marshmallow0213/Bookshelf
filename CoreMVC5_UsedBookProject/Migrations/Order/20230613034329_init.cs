using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC5_UsedBookProject.Migrations.Order
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChangeOrder",
                columns: table => new
                {
                    ChangeOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Buyer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    denyreason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeOrder", x => x.ChangeOrderId);
                });

            migrationBuilder.CreateTable(
                name: "MoneyOrder",
                columns: table => new
                {
                    MoneyOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    denyreason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Buyer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyOrder", x => x.MoneyOrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeOrder");

            migrationBuilder.DropTable(
                name: "MoneyOrder");
        }
    }
}
