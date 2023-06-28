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
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DenyReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shoppingcarts",
                columns: table => new
                {
                    ShoppingcartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoppingcarts", x => x.ShoppingcartId);
                    table.ForeignKey(
                        name: "FK_Shoppingcarts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shoppingcarts_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    { "U001", "kevinxi@gmail.com", "Admin0001", "Admin0001", "$2a$11$FpmWDffIDO9DHzi4nd46xuJcZrxxGsvh77NL5u/aRI1hruuP7LKwa", "0925-155222", "UserIcon.png" },
                    { "U002", "marylee@gmail.com", "Admin0002", "Admin0002", "$2a$11$IkjpFOr2ZnWORkS.XxfX2ezwg25chZDGE3q9YWLGLoZGRwV.ZzXrS", "0935-123123", "UserIcon.png" },
                    { "U003", "johnwei@gmail.com", "Admin0003", "Admin0003", "$2a$11$XjfS5fyle05kHNE7KPrNM./hK1YzETvbqmGbCJaXhlWRy5.kC8TOu", "0955-456456", "UserIcon.png" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Author", "ContentText", "CreateBy", "CreateDate", "Degree", "EditDate", "ISBN", "Image1", "Image2", "PublicationDate", "Publisher", "Status", "Title", "Trade", "UnitPrice" },
                values: new object[,]
                {
                    { "P001", "作者", "Context1", "U001", new DateTime(2023, 6, 28, 14, 43, 18, 238, DateTimeKind.Local).AddTicks(9163), "二手", new DateTime(2023, 6, 28, 14, 43, 18, 240, DateTimeKind.Local).AddTicks(5314), "9876543210", "example.jpg", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "2023-01-01", "出版社", "未上架", "Book1", "金錢", 500m },
                    { "P002", "作者", "Context2", "U001", new DateTime(2023, 6, 28, 14, 43, 18, 240, DateTimeKind.Local).AddTicks(6023), "二手", new DateTime(2023, 6, 28, 14, 43, 18, 240, DateTimeKind.Local).AddTicks(6029), "9876543211", "example.jpg", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "2023-01-01", "出版社", "未上架", "Book2", "金錢", 500m },
                    { "P003", "作者", "Context3", "U001", new DateTime(2023, 6, 28, 14, 43, 18, 240, DateTimeKind.Local).AddTicks(6036), "二手", new DateTime(2023, 6, 28, 14, 43, 18, 240, DateTimeKind.Local).AddTicks(6037), "9876543212", "example.jpg", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "2023-01-01", "出版社", "未上架", "Book3", "以物易物", -1m }
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
                table: "Orders",
                columns: new[] { "OrderId", "BuyerId", "CreateDate", "DenyReason", "ProductId", "SellerId", "Status", "Trade", "UnitPrice" },
                values: new object[] { "O001", "U002", new DateTime(2023, 6, 28, 14, 43, 18, 241, DateTimeKind.Local).AddTicks(9447), "none", "P001", "U001", "待確認", "金錢", 500m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "BuyerId", "CreateDate", "DenyReason", "ProductId", "SellerId", "Status", "Trade", "UnitPrice" },
                values: new object[] { "O002", "U002", new DateTime(2023, 6, 28, 14, 43, 18, 241, DateTimeKind.Local).AddTicks(9761), "none", "P002", "U001", "待確認", "金錢", 500m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "BuyerId", "CreateDate", "DenyReason", "ProductId", "SellerId", "Status", "Trade", "UnitPrice" },
                values: new object[] { "O003", "U002", new DateTime(2023, 6, 28, 14, 43, 18, 241, DateTimeKind.Local).AddTicks(9768), "none", "P003", "U001", "待確認", "以物易物", -1m });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreateBy",
                table: "Products",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Shoppingcarts_Id",
                table: "Shoppingcarts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shoppingcarts_ProductId",
                table: "Shoppingcarts",
                column: "ProductId");

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
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Shoppingcarts");

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
