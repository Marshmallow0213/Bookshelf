using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC5_UsedBookProject.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserIcon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicationDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Users_CreateBy",
                        column: x => x.CreateBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderByBarters",
                columns: table => new
                {
                    OrderByBarterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DenyReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    OrderByMoneyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DenyReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "R001", "Seller" },
                    { "R002", "Buyer" },
                    { "R003", "Administrator" },
                    { "R004", "" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Nickname", "Password", "PhoneNo", "UserIcon" },
                values: new object[,]
                {
                    { "U001", "kevinxi@gmail.com", "Admin0001", "Admin0001", "$2a$11$xBwl7sf6jXKknzq54gaZ0.PcNG13RhiaNP04AlMma.EItNyvbvElC", "0925-155222", "無圖片" },
                    { "U002", "marylee@gmail.com", "Admin0002", "Admin0002", "$2a$11$qIxo9QEyzth2iuY/no/KXeObz80PkFJ6OApBOCcM5GLeNV3BjWFNm", "0935-123123", "無圖片" },
                    { "U003", "johnwei@gmail.com", "Admin0003", "Admin0003", "$2a$11$q/.wFi9M031jdZRSXxRdyeS6nbwpT9KRyTvocGRUQEJ7dU/KhFTgW", "0955-456456", "無圖片" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Author", "ContentText", "CreateBy", "CreateDate", "Degree", "EditDate", "ISBN", "Image1", "Image2", "PublicationDate", "Publisher", "Status", "Title", "Trade", "UnitPrice" },
                values: new object[,]
                {
                    { "P001", "作者", "Context1", "U001", new DateTime(2023, 6, 22, 18, 35, 9, 955, DateTimeKind.Local).AddTicks(9388), "二手", new DateTime(2023, 6, 22, 18, 35, 9, 957, DateTimeKind.Local).AddTicks(3992), "9876543210", "example.jpg", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "2023-01-01", "出版社", "未上架", "Book1", "金錢", 500m },
                    { "P002", "作者", "Context2", "U001", new DateTime(2023, 6, 22, 18, 35, 9, 957, DateTimeKind.Local).AddTicks(4692), "二手", new DateTime(2023, 6, 22, 18, 35, 9, 957, DateTimeKind.Local).AddTicks(4697), "9876543211", "example.jpg", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "2023-01-01", "出版社", "未上架", "Book2", "金錢", 500m },
                    { "P003", "作者", "Context3", "U001", new DateTime(2023, 6, 22, 18, 35, 9, 957, DateTimeKind.Local).AddTicks(4704), "二手", new DateTime(2023, 6, 22, 18, 35, 9, 957, DateTimeKind.Local).AddTicks(4705), "9876543212", "example.jpg", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "2023-01-01", "出版社", "未上架", "Book3", "以物易物", -1m }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "R001", "U001" },
                    { "R002", "U001" },
                    { "R003", "U001" },
                    { "R001", "U002" },
                    { "R002", "U002" },
                    { "R003", "U002" },
                    { "R001", "U003" },
                    { "R002", "U003" },
                    { "R003", "U003" }
                });

            migrationBuilder.InsertData(
                table: "OrderByBarters",
                columns: new[] { "OrderByBarterId", "BuyerId", "CreateDate", "DenyReason", "ProductId", "SellerId", "Status" },
                values: new object[] { "OB001", "U003", new DateTime(2023, 6, 22, 18, 35, 9, 958, DateTimeKind.Local).AddTicks(8436), "none", "P003", "U001", "待確認" });

            migrationBuilder.InsertData(
                table: "OrderByMoneys",
                columns: new[] { "OrderByMoneyId", "BuyerId", "CreateDate", "DenyReason", "ProductId", "SellerId", "Status", "UnitPrice" },
                values: new object[] { "OM002", "U002", new DateTime(2023, 6, 22, 18, 35, 9, 958, DateTimeKind.Local).AddTicks(6587), "none", "P002", "U001", "待確認", 500m });

            migrationBuilder.InsertData(
                table: "OrderByMoneys",
                columns: new[] { "OrderByMoneyId", "BuyerId", "CreateDate", "DenyReason", "ProductId", "SellerId", "Status", "UnitPrice" },
                values: new object[] { "OM001", "U002", new DateTime(2023, 6, 22, 18, 35, 9, 958, DateTimeKind.Local).AddTicks(6297), "none", "P001", "U001", "待確認", 500m });

            migrationBuilder.CreateIndex(
                name: "IX_OrderByBarters_ProductId",
                table: "OrderByBarters",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderByMoneys_ProductId",
                table: "OrderByMoneys",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreateBy",
                table: "Products",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderByBarters");

            migrationBuilder.DropTable(
                name: "OrderByMoneys");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
