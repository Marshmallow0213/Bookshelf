using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC5_UsedBookProject.Migrations.Product
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PublicationDate = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContentText = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    Image1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image3 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image4 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image5 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image6 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image7 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image8 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image9 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Trade = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "OrderByBarters",
                columns: table => new
                {
                    OrderByBarterId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DenyReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderByBarters", x => x.OrderByBarterId);
                    table.ForeignKey(
                        name: "FK_OrderByBarters_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderByMoneys",
                columns: table => new
                {
                    OrderByMoneyId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DenyReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderByMoneys", x => x.OrderByMoneyId);
                    table.ForeignKey(
                        name: "FK_OrderByMoneys_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Author", "ContentText", "CreateBy", "CreateDate", "Degree", "EditDate", "ISBN", "Image1", "Image2", "Image3", "Image4", "Image5", "Image6", "Image7", "Image8", "Image9", "PublicationDate", "Publisher", "Status", "Title", "Trade", "UnitPrice" },
                values: new object[] { "P001", "作者", "Context1", "U001", new DateTime(2023, 6, 9, 16, 56, 2, 553, DateTimeKind.Local).AddTicks(1099), "二手", new DateTime(2023, 6, 9, 16, 56, 2, 555, DateTimeKind.Local).AddTicks(8480), "9876543210", "example.jpg", "無圖片", "無圖片", "無圖片", "無圖片", "無圖片", "無圖片", "無圖片", "無圖片", "2023-01-01", "出版社", "未上架", "Book1", "金錢", 500m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Author", "ContentText", "CreateBy", "CreateDate", "Degree", "EditDate", "ISBN", "Image1", "Image2", "Image3", "Image4", "Image5", "Image6", "Image7", "Image8", "Image9", "PublicationDate", "Publisher", "Status", "Title", "Trade", "UnitPrice" },
                values: new object[] { "P002", "作者", "Context2", "U001", new DateTime(2023, 6, 9, 16, 56, 2, 555, DateTimeKind.Local).AddTicks(9209), "二手", new DateTime(2023, 6, 9, 16, 56, 2, 555, DateTimeKind.Local).AddTicks(9214), "9876543211", "example.jpg", "無圖片", "無圖片", "無圖片", "無圖片", "無圖片", "無圖片", "無圖片", "無圖片", "2023-01-01", "出版社", "未上架", "Book2", "金錢", 500m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Author", "ContentText", "CreateBy", "CreateDate", "Degree", "EditDate", "ISBN", "Image1", "Image2", "Image3", "Image4", "Image5", "Image6", "Image7", "Image8", "Image9", "PublicationDate", "Publisher", "Status", "Title", "Trade", "UnitPrice" },
                values: new object[] { "P003", "作者", "Context3", "U001", new DateTime(2023, 6, 9, 16, 56, 2, 555, DateTimeKind.Local).AddTicks(9222), "二手", new DateTime(2023, 6, 9, 16, 56, 2, 555, DateTimeKind.Local).AddTicks(9223), "9876543212", "example.jpg", "無圖片", "無圖片", "無圖片", "無圖片", "無圖片", "無圖片", "無圖片", "無圖片", "2023-01-01", "出版社", "未上架", "Book3", "以物易物", -1m });

            migrationBuilder.InsertData(
                table: "OrderByBarters",
                columns: new[] { "OrderByBarterId", "BuyerId", "CreateDate", "DenyReason", "ProductId", "SellerId", "Status" },
                values: new object[] { "OB001", "U003", new DateTime(2023, 6, 9, 16, 56, 2, 557, DateTimeKind.Local).AddTicks(3971), "none", "P003", "U001", "待確認" });

            migrationBuilder.InsertData(
                table: "OrderByMoneys",
                columns: new[] { "OrderByMoneyId", "BuyerId", "CreateDate", "DenyReason", "ProductId", "SellerId", "Status", "UnitPrice" },
                values: new object[] { "OM002", "U002", new DateTime(2023, 6, 9, 16, 56, 2, 557, DateTimeKind.Local).AddTicks(2079), "none", "P002", "U001", "待確認", 500m });

            migrationBuilder.InsertData(
                table: "OrderByMoneys",
                columns: new[] { "OrderByMoneyId", "BuyerId", "CreateDate", "DenyReason", "ProductId", "SellerId", "Status", "UnitPrice" },
                values: new object[] { "OM001", "U002", new DateTime(2023, 6, 9, 16, 56, 2, 557, DateTimeKind.Local).AddTicks(1789), "none", "P001", "U001", "待確認", 500m });

            migrationBuilder.CreateIndex(
                name: "IX_OrderByBarters_ProductId",
                table: "OrderByBarters",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderByMoneys_ProductId",
                table: "OrderByMoneys",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderByBarters");

            migrationBuilder.DropTable(
                name: "OrderByMoneys");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
