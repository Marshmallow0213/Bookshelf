using System;
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maintitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adminlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_announcements", x => x.Id);
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
                name: "ToDoListViewModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maintitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoListViewModels", x => x.Id);
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
                    ListId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    AdminlistId = table.Column<int>(type: "int", nullable: true),
                    ToDoListViewModelsId = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_AdminlistRoles_ToDoListViewModels_ToDoListViewModelsId",
                        column: x => x.ToDoListViewModelsId,
                        principalTable: "ToDoListViewModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    { 1, "checkT" },
                    { 2, "checkF" },
                    { 3, "done" },
                    { 4, "undone" }
                });

            migrationBuilder.InsertData(
                table: "Adminlists",
                columns: new[] { "Id", "Date", "Maintitle", "Subtitle" },
                values: new object[,]
                {
                    { 10, "2023/07/19", "按鈕樣式", "找尋不同按鈕以做為以後界面更新時的備選方案" },
                    { 9, "2023/07/19", "購物車介面", "購物車介面想要做更換，請聯絡開發人員做修改" },
                    { 8, "2023/07/18", "管理員登入介面公告", "對於新進管理人員許多人不知道隱藏登入位置，要在LINE群做公告" },
                    { 7, "2023/07/17", "主頁介面更改", "首頁登入與搜尋框位置做調換" },
                    { 6, "2023/07/17", "首頁資訊", "首頁東西太多，我們的目的是需要將買賣與交換的物品放在專屬賣場，原本的主頁面要維持簡潔，請各位討論好後聯絡開發人員做修改" },
                    { 5, "2023/07/16", "管理員介面測試", "管理員介面做優化，測試開啟速度" },
                    { 4, "2023/07/15", "買賣書籍跑版", "買賣書籍頁面跑版，聯絡開發人員做修改" },
                    { 3, "2023/07/14", "介面設計", "介面太醜需要調整，聯絡開發人員做修改" },
                    { 2, "2023/07/14", "交換書籍測試", "交換書籍部分測試部分已釋出，所有管理人員測試完後在LINE群回復使用心得" },
                    { 1, "2023/07/13", "頁面修改", "聯絡開發人員將登入做修改後請他也做介面優化" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "R003", "Suspended Administrator" },
                    { "R002", "common Administrator" },
                    { "R001", "Top Administrator" }
                });

            migrationBuilder.InsertData(
                table: "TextValue",
                columns: new[] { "Id", "Image1", "Image2", "Image3", "TextValue" },
                values: new object[] { "T001", "Carousel.jpg", "CarouselSecond.jpg", "CarouselThird.jpg", "二手書交換平台" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Nickname", "Password", "PhoneNo" },
                values: new object[,]
                {
                    { "B003", "Neverloses@bookshelf.com", "Neverloses@bookshelf.com", "甲甲志", "b70d01e0fac31e93a760021e7cf970d4", "0987-587-587" },
                    { "B002", "Tony@bookshelf.com", "Tony@bookshelf.com", "Tony", "213ffb90868d8b9c58aae64988f642f1", "0933-941-941" },
                    { "B001", "Potatodog@bookshelf.com", "Potatodog@bookshelf.com", "馬鈴薯狗", "d5e10bd206b5f1aca583f37be2566e70", "0900-951-456" }
                });

            migrationBuilder.InsertData(
                table: "announcements",
                columns: new[] { "Id", "CreatedAt", "Message" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "系統將於2023/08/16中午12:00開放主頁面圖片徵選，各位想要更換自己喜愛圖片的機會就在這次!有興趣者私訊Potatodog@bookshelf.com，最高票的3張圖片將獲得獎金2000,1000,500元的獎勵" },
                    { 2, new DateTime(2023, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "本平台舉辦按鈕繪畫比賽，獲勝者可以獲取高達總金額8000元比賽獎金!有興趣者致電0900-951-456" },
                    { 3, new DateTime(2023, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "系統將在2023/09/29中午12:00到17:00進行維護更新，屆時將暫時停止伺服器運營。" }
                });

            migrationBuilder.InsertData(
                table: "AdminlistRoles",
                columns: new[] { "ListId", "RoleId", "AdminlistId", "ToDoListViewModelsId" },
                values: new object[,]
                {
                    { 1, 4, null, null },
                    { 2, 4, null, null },
                    { 3, 4, null, null },
                    { 4, 4, null, null },
                    { 5, 4, null, null },
                    { 6, 4, null, null },
                    { 7, 4, null, null },
                    { 8, 4, null, null },
                    { 9, 4, null, null },
                    { 10, 4, null, null }
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
                name: "IX_AdminlistRoles_ToDoListViewModelsId",
                table: "AdminlistRoles",
                column: "ToDoListViewModelsId");

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
                name: "announcements");

            migrationBuilder.DropTable(
                name: "TextValue");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Adminlists");

            migrationBuilder.DropTable(
                name: "AdminRoles");

            migrationBuilder.DropTable(
                name: "ToDoListViewModels");

            migrationBuilder.DropTable(
                name: "AdministratorUserHomePage");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
