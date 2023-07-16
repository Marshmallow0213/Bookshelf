using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC5_UsedBookProject.Migrations.AdminAccount
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdministratorUserHomePage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministratorUserHomePage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adminlists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Maintitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adminlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TextValue",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TextValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminlistRoles",
                columns: table => new
                {
                    ListId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminlistId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminlistRoles", x => new { x.ListId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AdminlistRoles_Adminlists_AdminlistId",
                        column: x => x.AdminlistId,
                        principalTable: "Adminlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdminlistRoles_AdminRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AdminRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdministratorUserHomePageId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_AdministratorUserHomePage_AdministratorUserHomePageId",
                        column: x => x.AdministratorUserHomePageId,
                        principalTable: "AdministratorUserHomePage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.InsertData(
                table: "AdminRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "R001", "undone" },
                    { "R002", "done" }
                });

            migrationBuilder.InsertData(
                table: "Adminlists",
                columns: new[] { "Id", "Maintitle", "subtitle" },
                values: new object[,]
                {
                    { "2023/07/16", "帶烏龜看醫生", "帶烏龜到334桃園市八德區介壽路一段248-1號(丹尼爾動物醫院)看醫生，並且要確定身高體重以及服藥次數" },
                    { "2023/07/18", "買便當回管理員室", "因為平常樓下便當實在太難吃了，所以老闆希望可以買山下平常聚會地點的羊肉爐" },
                    { "2023/07/20", "倒垃圾", "管理員室的垃圾已經推積如山，所以要盡快完成" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "R001", "Top Administrator" },
                    { "R002", "common Administrator" },
                    { "R003", "Suspended Administrator" }
                });

            migrationBuilder.InsertData(
                table: "TextValue",
                columns: new[] { "Id", "Image1", "Image2", "Image3", "TextValue" },
                values: new object[] { "T001", "Deafult.jpg", "Deafult.jpg", "Deafult.jpg", "二手書交換平台" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Nickname", "Password", "PhoneNo" },
                values: new object[,]
                {
                    { "B001", "Potatodog@bookshelf.com", "Potatodog@bookshelf.com", "馬鈴薯狗", "d5e10bd206b5f1aca583f37be2566e70", "0900-951-456" },
                    { "B002", "Tony@bookshelf.com", "Tony@bookshelf.com", "Tony", "213ffb90868d8b9c58aae64988f642f1", "0933-941-941" },
                    { "B003", "Neverloses@bookshelf.com", "Neverloses@bookshelf.com", "甲甲志", "b70d01e0fac31e93a760021e7cf970d4", "0987-587-587" }
                });

            migrationBuilder.InsertData(
                table: "AdminlistRoles",
                columns: new[] { "ListId", "RoleId", "AdminlistId" },
                values: new object[,]
                {
                    { "2023/07/16", "R001", null },
                    { "2023/07/18", "R001", null },
                    { "2023/07/20", "R001", null }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "AdministratorUserHomePageId" },
                values: new object[,]
                {
                    { "R001", "B001", null },
                    { "R001", "B002", null },
                    { "R001", "B003", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminlistRoles_AdminlistId",
                table: "AdminlistRoles",
                column: "AdminlistId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminlistRoles_RoleId",
                table: "AdminlistRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_AdministratorUserHomePageId",
                table: "UserRoles",
                column: "AdministratorUserHomePageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminlistRoles");

            migrationBuilder.DropTable(
                name: "TextValue");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Adminlists");

            migrationBuilder.DropTable(
                name: "AdminRoles");

            migrationBuilder.DropTable(
                name: "AdministratorUserHomePage");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
