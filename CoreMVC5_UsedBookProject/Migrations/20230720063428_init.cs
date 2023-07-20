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
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TradingRemarque = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
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
                name: "Wishes",
                columns: table => new
                {
                    WishId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishes", x => x.WishId);
                    table.ForeignKey(
                        name: "FK_Wishes_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BarterOrders",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DenyReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarterOrders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_BarterOrders_Products_BuyerProductId",
                        column: x => x.BuyerProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                    { "R001", "User" },
                    { "R002", "Suspension" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Nickname", "Password", "PhoneNo", "UserIcon" },
                values: new object[,]
                {
                    { "U001", "B10802001@g.chu.edu.tw", "B10802001", "B10802001", "$2a$11$88FJ/BlcioDwTRu7u6DZ/uzQsdSRJsyZFo0/sAHc5zZYOuD6ycqla", "0978-042-241", "UserIcon.png" },
                    { "U002", "B10802002@g.chu.edu.tw", "B10802002", "B10802002", "$2a$11$p8jIp0deQO1hcSB15t3eYepnr/UgUCEA1eXcnq42IzQoEeWLD7Pw2", "0978-042-242", "UserIcon.png" },
                    { "U003", "B10802003@g.chu.edu.tw", "B10802003", "B10802003", "$2a$11$f0z0/stFt7P3JynX2iIXheKIl4nsG/pOTr86idMIoPgi8gZdgL93O", "0978-042-243", "UserIcon.png" },
                    { "U004", "B10802004@g.chu.edu.tw", "B10802004", "B10802004", "$2a$11$DYakYhDnbC4E54/aQ8QntuNkUPOheH.aj8gbkZN6wBFv2.a6Sa8hW", "0978-042-244", "UserIcon.png" },
                    { "U005", "B10802005@g.chu.edu.tw", "B10802005", "B10802005", "$2a$11$JZzxPSTKUdm1df2Elr2QtOoqA5QkQNoCgDO2teO0.5vVoEMXLh6Ae", "0978-042-245", "UserIcon.png" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Author", "ContentText", "CreateBy", "CreateDate", "Degree", "EditDate", "ISBN", "ImageList", "Publisher", "Status", "Title", "Trade", "TradingRemarque", "UnitPrice" },
                values: new object[,]
                {
                    { "P001", "Ralph P. Grimaldi (author)", "這第五版持續改進了使它成為市場領導者的特點。該教材提供了靈活的組織，使教師能夠根據自己的課程進行調整。這本書既完整又細心，並且繼續強調算法和應用。優秀的練習題集使學生在練習中完善技能。這個新版繼續提供了許多計算機科學應用，使其成為為學生準備進階學習的理想教材。", "U001", new DateTime(2023, 7, 20, 14, 34, 27, 750, DateTimeKind.Local).AddTicks(3222), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(6447), "9781292022796", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "Addison Wesley", "已上架", "Discrete and Combinatorial Mathematics: An Applied Introduction, 5/e (IE-Paperback)", "買賣與交換", "中華大學第一餐廳", 1519m },
                    { "P018", "郭皇甫, 蔡雨錡, 曾吉弘", "．樂高EV3機器人結合了簡單易用的圖形化程式環境，以及更強更快的控制核心與感測器，搭配後馬上就能完成您的第一台機器人。\r\n\r\n　　．結合樂高各種不同的零件，讓您的機器人可以完成各種複雜的動作功能，在實作中理解各種機械與物理原理。\r\n\r\n　　．本書內含數十個程式範例，包含機器人行為設計、感測器、音效以及藍牙遙控等許多整合式機器人應用，是您在學習機器人的路程中一本實用的專題指南，非常適合各級教學單位使用。", "U003", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7247), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7248), "9789864050208", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "馥林文化", "已上架", "機器人程式超簡單：LEGO MINDSTORMS EV3動手作（專題卷）", "買賣與交換", "中華大學第一餐廳", 432m },
                    { "P017", "鳥哥", "Linux經典學習書！\r\n　　本書前三版均蟬聯電腦專業書籍Linux暢銷排行榜Top1，為地表上最暢銷的Linux中文書籍！\r\n　　您是有意學習Linux的小菜鳥，卻不知如何下手？\r\n　　您是遨遊Linux的老鳥，想要一本資料豐富的工具書？\r\n　　《鳥哥的Linux私房菜基礎學習篇》絕對是最佳選擇！\r\n\r\n　　※鳥哥傾囊相授，內容由淺入深\r\n　　書中包含了鳥哥從完全不懂Linux到現在的所有歷程，鳥哥將這幾年來的所知所學傾囊相授，以最淺顯易懂的文字帶領您進入Linux的世界。\r\n\r\n　　※按部就班，打好基礎的第一步\r\n　　本書劃分為五大部分，每個部分都有相關性的特色，涵蓋：Linux的規劃與安裝，認識Linux檔案、目錄與磁碟格式，學習Shell與Shell Scripts，Linux使用者管理與Linux系統管理員，依序學習，讓您奠定Linux的基礎，跨出成功的第一步。\r\n\r\n　　※用心改版，提供您更新的技術\r\n　　《鳥哥的Linux私房菜基礎學習篇-第四版》提供近期更新的技術，包括：核心版本的升級建議、\r\n　　虛擬系統的操作、GPT 分割表格式處理、XFS 檔案系統的實際操作使用、systemd 服務的管理、\r\n　　日誌格式的更新、grub2 開機管理程式的說明、nmcli文字指令操作網路參數的方式等等，內容更加精彩！", "U003", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7244), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7245), "9789863478652", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "碁峰", "已上架", "鳥哥的Linux私房菜：基礎學習篇(附DVD一片)(第四版)", "買賣與交換", "中華大學第一餐廳", 882m },
                    { "P016", "LIANG", "This text is intended for a 1-semester CS1 course sequence. The Brief Version contains the first 18 chapters of the Comprehensive Version. The first 13 chapters are appropriate for preparing the AP Computer Science exam.\r\n\r\n　　For courses in Java Programming.\r\n\r\n　　A fundamentals-first introduction to basic programming concepts and techniques\r\n\r\n　　Designed to support an introductory programming course, Introduction to Java Programming and Data Structuresteaches you concepts of problem-solving and object-orientated programming using a fundamentals-first approach. Beginner programmers learn critical problem-solving techniques then move on to grasp the key concepts of object-oriented, GUI programming, data structures, and Web programming. This course approaches Java GUI programming using JavaFX, which has replaced Swing as the new GUI tool for developing cross-platform-rich Internet applications and is simpler to learn and use. The 11th edition has been completely revised to enhance clarity and presentation, and includes new and expanded content, examples, and exercises.", "U003", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7241), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7242), "9781292221878", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "全華圖書", "已上架", "INTRODUCTION TO JAVA PROGRAMMING: COMPREHENSIVE VERSION 11/E (GE)", "買賣與交換", "中華大學第一餐廳", 1400m },
                    { "P015", "陳會安", "本書採用App Inventor最新版本的雲端開發平台（需Internet連線），只需與網際網路連線，就可以輕鬆使用App Inventor 2中文版開發Android App。\r\n\r\n本書架構由淺入深，從Android和App Inventor 2開始，詳細說明Android基礎程式設計，強調布局和使用介面的互動設計，從按鈕、標籤與文字輸入盒組件開始，到選擇功能的介面組件和圖片顯示，然後是清單介面，訊息與對話框，完整說明使用介面的建立，以及如何與使用者進行互動。\r\n\r\n本書說明如何啟動其他畫面和行動裝置的內建Apps，幫助讀者建立多畫面和整合內建App的應用程式，最後使用多個綜合應用範例來說明Android App開發的實作技巧。\r\n\r\n實作範例介紹調整變數的使用方式，減少前版區域變數的使用，改以全域變數實作。並介紹統計圖表的繪製、OpenData的連結操作及人工智慧等熱門主題。", "U003", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7231), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7239), "9786263281790", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "全華", "已上架", "App Inventor 2程式設計與應用：開發Android App一學就上手（第五版）（附範例光碟）", "買賣與交換", "中華大學第一餐廳", 560m },
                    { "P014", "李開偉", "本書內容是作者依據科技發展趨勢及目前國內產業的特色編寫的，主要包含平板電腦、觸控、電子、著作權等相關科技產業內容，讓讀者習得最新科技產業資訊及英文字彙。針對英文字彙的加強，每課皆有「Families of Vocabulary」（字彙家族）單元，提升讀者記憶的字彙量，並增進科技英文閱讀能力。", "U003", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7229), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7230), "9789864632961", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "全華圖書", "已上架", "科技英文導讀(第五版)(附課文朗讀CD)", "買賣與交換", "中華大學第一餐廳", 437m },
                    { "P013", "Alan Dennis (Author), Barbara Haley Wixom (Author), David Tegarden (Author)", "You can’t truly understand Systems Analysis and Design (SAD) by only reading about it; you have to do it. In Systems Analysis and Design, Third Edition, Dennis, Wixom, and Roth offer a hands-on approach to actually doing SAD. Building on their experience as professional systems analysts and award-winning teachers, these three authors capture the experience of actually developing and analyzing systems. They focus on the core set of skills that all analysts must possess––from gathering requirements and modeling business needs, to creating blueprints for how the system should be built.", "U003", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7225), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7226), "9780470074787", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "Wiley", "已上架", "Systems Analysis and Design with UML 3rd", "買賣與交換", "中華大學第一餐廳", 6965m },
                    { "P011", "俞征武", "追求 簡單、自然、猜\r\n\r\n演算法是利用電腦解決問題的技巧之一, 本書用輕鬆的對話手法, 希望幫助學生「簡單」且「自然」地掌握演算法的基本觀念，並養成「猜」的習慣，日後可以主動思考、嘗試解決問題。\r\n\r\n◎ 輕鬆學習寫程式的基本策略\r\n◎ 介紹常用的程式設計技巧\r\n◎ 自然地掌握解決問題的精神\r\n◎ 刻意忽略繁瑣不重要的演算細節\r\n◎ 練習發明新演算法的猜測習慣", "U002", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7220), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7221), "9789863123200", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "旗標", "已上架", "資料結構與演算法", "買賣與交換", "中華大學第一餐廳", 450m },
                    { "P010", "胡昭民,吳燦銘", "從產業經濟面的發展趨勢來分析，遊戲產業近年來的表現相當亮眼，市場規模足以和影視娛樂產業並驅。其中的電子競技遊戲更是風靡全球，全新的娛樂型態讓每個遊戲都有死忠鐵粉，無不好奇競相加入，然而玩家們必須要知道的前提是，遊戲本身才是電競賽事的真正靈魂，即使是戰功彪炳的電競高手，除了快手和快腦外，若能洞悉遊戲中各式複雜的關卡與眉角間的設計，必定能如虎添翼地增加打怪過關的勝率。\r\n \r\n　　這是一本提供入門者進入遊戲設計與電競領域的實用教材，內容理論與實務並重，章節編排除了介紹遊戲開發過程中的各種工具，及許多開發成品的展示外，還包含從遊戲類型到產業的說明、遊戲設計鋪陳方式解析、遊戲開發工具、2D、3D、數學、物理現象模擬、遊戲團隊組成藍圖…等主題，這次改版也將電競導論及工具納入，並探討大數據與遊戲行銷關聯性等議題。", "U002", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7217), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7218), "9789864345120", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "博碩", "已上架", "遊戲設計與電競運動概論", "買賣與交換", "中華大學第一餐廳", 420m },
                    { "P009", "鄭苑鳳、吳燦銘", "本書結合理論與實務，全方面介紹多媒體理論與實際應用層面。一開始從數位內容的角度談論廣義多媒體的組成、理論及相關的硬體設備，也從多媒體的應用實例來提升讀者對多媒體創作的興趣。依序介紹各種多媒體素材的基礎理論、特性及相關軟體，包括：文字媒體、影像媒體、音訊媒體、視訊媒體及動畫媒體等。\r\n\r\n在實務應用方面，期望能讓讀者在短時間了解各種多媒體軟體技術的精華，軟體實作的主題包括：影像處理設計、視訊剪輯製作、2D/3D動畫實務。再則將重點放在作品的完成及平台的展現，引導讀者將所學會的多媒體相關技術，進行更生動及多元化的應用，例如多媒體光碟製作、網頁設計及數位學習。最後透過網際網路的平台及網路通訊的能力，進行數位性整合，使文字、聲音、影像、圖片、視訊及動畫，豐富地整合在一起，將多媒體與網路行銷加以結合，徹底發揮應用。豐富的內容與資訊，可作為多媒體製作相關課程的重要教材，更可帶領讀者在多媒體設計領域中打下一個完整與良好的基礎。", "U002", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7214), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7215), "9789862016855", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "博碩", "已上架", "數位多媒體概論", "買賣與交換", "中華大學第一餐廳", 580m },
                    { "P008", "Ellis Horowitz, Sartaj Sahni, Susan Anderson-Freed", "新版的經典資料結構教材！本書全面且技術嚴謹地介紹了數組、堆疊、佇列、鏈結串列、樹和圖等資料結構，以及排序和雜湊等基礎軟體的技巧。此外，本書還介紹了高級或特殊的資料結構，如優先佇列、高效二元搜尋樹、多路搜尋樹和數位搜尋結構。本書現在還討論了一些新的主題，如加權偏左樹、配對堆、對稱極值堆、區間堆、自頂向下伸展樹、B+樹和後綴樹。紅黑樹的內容更加易於理解。多路字典樹的部分已經大幅擴展，並討論了幾種字典樹的變體及其在互聯網封包轉發中的應用。", "U002", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7209), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7209), "9780929306407", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "Silicon Press", "已上架", "Fundamentals of Data Structures in C, 2/e (Paperback)", "買賣與交換", "中華大學第一餐廳", 1362m },
                    { "P012", "KUROSE, ROSS", "A top-down,layered approach to computer networking. Unique among computernetworking texts, the 8th Edition, Global Edition, of thepopular Computer Networking: A Top Down Approach buildson the authors' long tradition of teaching this complex subject through alayered approach in a \"top-down manner.\" The text works its way from theapplication layer down toward the physical layer, motivating students byexposing them to important concepts early in their study of networking.Focusing on the Internet and the fundamentally important issues of networking,this text provides an excellent foundation for students in computer science andelectrical engineering, without requiring extensive knowledge of programming ormathematics. The 8th Edition, Global Edition, has been updatedto reflect the most important and exciting recent advances in networking,including the importance of software-defined networking (SDN) and the rapidadoption of 4G/5G networks and the mobile applications they enable.", "U002", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7223), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7224), "9781292405469", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "全華圖書", "已上架", "COMPUTER NETWORKING: A TOP-DOWN APPROACH 8/E (GE)", "買賣與交換", "中華大學第一餐廳", 1500m },
                    { "P006", "健康資訊工程實驗室", "睜開眼，柔和的光線流入視野；普羅用手撐起身子，環顧四周，眼前的一切都與自己所熟悉的環境大不相同，陌生卻又美妙的感覺盈滿了他的周遭。\r\n\r\n　　「你醒啦？ 」順著聲音的來源轉過頭去，一隻美麗的精靈正投注著關心的眼神。\r\n\r\n　　「其實Python世界即將舉辦年度的世界大賽，每個Python世界的居民都可以自由參加。這不僅是Python 界的盛事，更是其他世界得以踏入Python幻境的唯一機會。因為我看你很有慧根，是個百年難得一見的奇才，所以選中你進入Python世界來參加這個大賽。」\r\n\r\n　　隨著美麗的精靈菲絲恩的帶領下，普羅走入奇幻的精靈世界，參與Python世界魔法大賽……\r\n\r\n　　本書使用故事的筆法，讓初次接觸程式語言或對Python有興趣之初學者可以輕易迅速的掌握Python的觀念及使用技巧。─鄭伯壎 教授", "U001", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7196), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7197), "9789864341047", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "博碩", "已上架", "菲絲恩教你學會Python(第二版)", "買賣與交換", "中華大學第一餐廳", 199m },
                    { "P002", "文淵閣工作室", "Python正宗入門NO.1 \r\n　　一本引領數以萬計讀者一探Python世界、 \r\n　　也讓老師用了再用的暢銷經典！ \r\n \r\n　　人工智慧與大數據時代、新課綱世代 \r\n　　跨入程式語言、鍛鍊邏輯思維， \r\n　　就從Python開始學！ \r\n \r\n　　Python是目前最熱門的程式語言，執行功能強大，但語法卻簡潔優雅、易於學習，更方便應用在許多專案實作上。它也沒有複雜的結構，程式易讀，且易於維護。 \r\n \r\n　　Python的應用範圍相當廣泛，無論是資訊蒐集、大數據分析、機器學習、網站建置，甚至是遊戲開發等，都能看到它的身影。本書以零基礎學習者的視角進行規劃，從最基本的認識程式語言與環境架設切入，再進到程式設計流程的完整學習，輔以觀念圖解、表格歸納，以及流程圖，深入淺出一窺Python程式語言與設計的奧妙！ \r\n \r\n　　要懂Python \r\n　　就要這樣真正的入門 \r\n　　徹底掌握程式語言與設計的核心！ \r\n \r\n　　■適合無程式設計經驗或想打好Python基礎者，從中了解運算思維精神，扎根程式設計學習，培養邏輯運算能力。循序漸進的內容涵蓋：認識運算思維、程式語言與設計、環境建置、變數、運算式、判斷式、迴圈、串列與元組、字典、函式與模組、演算法，以及檔案與例外處理…等，讓初學者無痛學習，輕鬆打好基本功。 \r\n \r\n　　■精心設計100題實作範例，輔以130題綜合演練，從做中學，快速學會每個學習重點，並能應用於實際專題中。範例大小適合讀者平時的學習，或每週固定時數的教學課程。 \r\n \r\n　　■融入精采的情境實例，貼近生活應用，也讓練習更有趣，如：數學運算、密碼判斷、成績評等、電影分級、百貨公司折扣戰、數字比大小、薪資計算表、成績單與業績報表列印、日期時間格式與溫度轉換、擲骰子遊戲、大樂透抽獎、搜尋中獎者、彩券對獎，以及血型個性、四季天氣、成績輸入、產品銷售、電費、世大運獎牌數與家庭支出查詢…等運用。 \r\n \r\n　　■提供更完整的演算法運作實例搭配流程圖，有利理解運作思路與規則，讓程式開發更加得心應手。 \r\n \r\n　　■收錄160分鐘的Python開發環境建置與語法入門影音教學，搭配書籍內容快速吸收，讓學習更有效率。 \r\n \r\n　　書附超值學習資源：160分鐘快速入門影音教學/範例程式檔/綜合演練參考解答 ", "U001", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7105), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7109), "9789865028190", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "碁峰", "已上架", "Python零基礎入門班(第三版)：一次打好程式設計、運算思維與邏輯訓練基本功(附160分鐘入門影音教學/範例程式)", "買賣與交換", "中華大學第一餐廳", 308m },
                    { "P003", "Nell Dale, John Lewis", "", "U001", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7114), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7115), "9781284155617", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "Jones and Bartlett", "已上架", "Computer Science Illuminated, 7/e (Paperback)", "買賣與交換", "中華大學第一餐廳", 1392m },
                    { "P004", "Paul Deitel (Author), Harvey Deitel (Author)", "For all basic-to-intermediate level courses in Visual C# programming.\r\n\r\nAn informative, engaging, challenging and entertaining introduction to Visual C#\r\n\r\nCreated by world-renowned programming instructors Paul and Harvey Deitel, Visual C# How to Program, Sixth Edition introduces students to the world of desktop, mobile and web app development with Microsoft’s® Visual C#® programming language. Students will use .NET platform and the Visual Studio® Integrated Development Environment to write, test, and debug applications and run them on a wide variety of Windows® devices.\r\n\r\nAt the heart of the book is the Deitel signature live-code approach―rather than using code snippets, the authors present concepts in the context of complete working programs followed by sample executions. Students begin by getting comfortable with the Visual Studio Community edition IDE and basic C# syntax. Next, they build their skills one step at a time, mastering control structures, classes, objects, methods, variables, arrays, and the core techniques of object-oriented programming. With this strong foundation in place, the authors introduce more sophisticated techniques, including searching, sorting, data structures, generics, and collections. Additional practice is provided through a broad range of example programs and exercises selected from computer science, business, education, social issues, personal utilities, sports, mathematics, puzzles, simulation, game playing, graphics, multimedia and many other areas.", "U001", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7117), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7118), "9780134601540", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "Pearson", "已上架", "Visual C# How to Program (Deitel Series) 6th", "買賣與交換", "中華大學第一餐廳", 3265m },
                    { "P005", "洪錦魁", "本書將在北京清華大學與台灣深石數位科技同步發行。這是一本專為沒有程式設計基礎的讀者設計的零基礎入門Python書籍，全書超過500程式實例，一步一步講解Python入門的基礎知識，同時也將應用範圍擴充至GUI(圖形使用者介面)設計、影像處理、圖表繪製。Python是一門可以很靈活使用的程式語言，本書特色在於對Python最基礎的知識使用了大量靈活的實例說明各種應用方法，讀者可以由這些程式實例事半功倍完成學會Python。", "U001", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7191), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7193), "9789865002237", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "深智數位", "已上架", "Python零基礎最強入門之路-王者歸來", "買賣與交換", "中華大學第一餐廳", 520m },
                    { "P007", "楊珮璐、宋強", "當然你可以和從前一樣，學習PHP以及Visual Studio中其它的語言，日子一樣輕鬆愉快。但當工作的負擔越來越大，程式結構日益複雜，但對於程式的高效及輕量的需求越趨嚴格時，一個DNA良好的語言可以讓你省下不少精神。\r\n\r\nPython是最優美的語言，也號稱Shell語言中的Scala，支援最豐富的資料型態以及最直覺又精簡的語法，更有大量的函數庫及協力廠商套件，在Facebook、Google等大型企業，Python早就是最多工程師使用的語言了。大數據時代來臨，Python更有Scikit、Numpy等package讓你無縫接軌，你終究還是要用Python，何不一開始就學？\r\n\r\n全書內容共分三篇：\r\n\r\n●入門篇：包含Python的認識和安裝、開發工具簡介、Python基本語法、數據結構與演算法、多媒體編程、系統應用、圖像處理和GUI編程等內容。\r\n\r\n●進階篇：包括用Python操作資料庫、進行Web開發、網路編程、科學計算等內容。\r\n\r\n●案例篇：以3個案例展現Python在Windows系統優越化、大數據處理和遊戲開發方面的應用。\r\n\r\n本書特色\r\n\r\n●以Python 3.x版本進行講解，並附上與2.x版本的相關說明，適合使用兩個版本的讀者參考應用。\r\n\r\n●包含用Python使用資料庫、進行Web開發、網路編譯、科學計算等進階領域。\r\n\r\n●以大量實例指導讀者逐步深入研究，並提供完整解釋，幫助讀者實際應用。\r\n\r\n●附有大量的圖表和插圖，力求減少長篇的理論介紹和公式推導，以便讀者透過實例和資料學習同時也能了解理論基礎。\r\n\r\n●提供三大案例，分別使用Python進行Window優質化，大數據處理和遊戲開發方面的應用。", "U002", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7206), "二手，保存良好", new DateTime(2023, 7, 20, 14, 34, 27, 752, DateTimeKind.Local).AddTicks(7207), "9789863756262", "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", "上奇科技", "已上架", "科學運算：Python程式理論與應用", "買賣與交換", "中華大學第一餐廳", 731m }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "R001", "U003" },
                    { "R001", "U001" },
                    { "R001", "U005" },
                    { "R001", "U002" },
                    { "R001", "U004" }
                });

            migrationBuilder.InsertData(
                table: "Wishes",
                columns: new[] { "WishId", "ISBN", "Id", "Title" },
                values: new object[,]
                {
                    { 5, "9789865028190", "U002", "Python零基礎入門班(第三版)：一次打好程式設計、運算思維與邏輯訓練基本功(附160分鐘入門影音教學/範例程式)" },
                    { 4, "9781292022796", "U002", "Discrete and Combinatorial Mathematics: An Applied Introduction, 5/e (IE-Paperback)" },
                    { 1, "9780470074787", "U001", "Systems Analysis and Design with UML 3rd" },
                    { 2, "9789864632961", "U001", "科技英文導讀(第五版)(附課文朗讀CD)" },
                    { 7, "9789863756262", "U003", "科學運算：Python程式理論與應用" },
                    { 8, "9780929306407", "U003", "Fundamentals of Data Structures in C, 2/e (Paperback)" },
                    { 9, "9789862016855", "U003", "數位多媒體概論" },
                    { 6, "9781284155617", "U002", "Computer Science Illuminated, 7/e (Paperback)" },
                    { 3, "9786263281790", "U001", "App Inventor 2程式設計與應用：開發Android App一學就上手（第五版）（附範例光碟）" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarterOrders_BuyerProductId",
                table: "BarterOrders",
                column: "BuyerProductId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Wishes_Id",
                table: "Wishes",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarterOrders");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Shoppingcarts");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Wishes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
