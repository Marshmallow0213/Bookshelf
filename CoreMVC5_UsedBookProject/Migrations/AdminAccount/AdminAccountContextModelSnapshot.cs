﻿// <auto-generated />
using System;
using CoreMVC5_UsedBookProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoreMVC5_UsedBookProject.Migrations.AdminAccount
{
    [DbContext(typeof(AdminAccountContext))]
    partial class AdminAccountContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdminRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdminRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "checkT"
                        },
                        new
                        {
                            Id = 2,
                            Name = "checkF"
                        },
                        new
                        {
                            Id = 3,
                            Name = "done"
                        },
                        new
                        {
                            Id = 4,
                            Name = "undone"
                        });
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdministratorRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = "R001",
                            Name = "Top Administrator"
                        },
                        new
                        {
                            Id = "R002",
                            Name = "common Administrator"
                        },
                        new
                        {
                            Id = "R003",
                            Name = "Suspended Administrator"
                        });
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdministratorUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "B001",
                            Email = "Potatodog@bookshelf.com",
                            Name = "Potatodog@bookshelf.com",
                            Nickname = "馬鈴薯狗",
                            Password = "d5e10bd206b5f1aca583f37be2566e70",
                            PhoneNo = "0900-951-456"
                        },
                        new
                        {
                            Id = "B002",
                            Email = "Tony@bookshelf.com",
                            Name = "Tony@bookshelf.com",
                            Nickname = "Tony",
                            Password = "213ffb90868d8b9c58aae64988f642f1",
                            PhoneNo = "0933-941-941"
                        },
                        new
                        {
                            Id = "B003",
                            Email = "Neverloses@bookshelf.com",
                            Name = "Neverloses@bookshelf.com",
                            Nickname = "甲甲志",
                            Password = "b70d01e0fac31e93a760021e7cf970d4",
                            PhoneNo = "0987-587-587"
                        });
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdministratorUserRoles", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdministratorUserHomePageId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("AdministratorUserHomePageId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "B001",
                            RoleId = "R001"
                        },
                        new
                        {
                            UserId = "B002",
                            RoleId = "R001"
                        },
                        new
                        {
                            UserId = "B003",
                            RoleId = "R001"
                        });
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.Adminlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maintitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subtitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Adminlists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = "2023/07/13",
                            Maintitle = "頁面修改",
                            Subtitle = "聯絡開發人員將登入做修改後請他也做介面優化"
                        },
                        new
                        {
                            Id = 2,
                            Date = "2023/07/14",
                            Maintitle = "交換書籍測試",
                            Subtitle = "交換書籍部分測試部分已釋出，所有管理人員測試完後在LINE群回復使用心得"
                        },
                        new
                        {
                            Id = 3,
                            Date = "2023/07/14",
                            Maintitle = "介面設計",
                            Subtitle = "介面太醜需要調整，聯絡開發人員做修改"
                        },
                        new
                        {
                            Id = 4,
                            Date = "2023/07/15",
                            Maintitle = "買賣書籍跑版",
                            Subtitle = "買賣書籍頁面跑版，聯絡開發人員做修改"
                        },
                        new
                        {
                            Id = 5,
                            Date = "2023/07/16",
                            Maintitle = "管理員介面測試",
                            Subtitle = "管理員介面做優化，測試開啟速度"
                        },
                        new
                        {
                            Id = 6,
                            Date = "2023/07/17",
                            Maintitle = "首頁資訊",
                            Subtitle = "首頁東西太多，我們的目的是需要將買賣與交換的物品放在專屬賣場，原本的主頁面要維持簡潔，請各位討論好後聯絡開發人員做修改"
                        },
                        new
                        {
                            Id = 7,
                            Date = "2023/07/17",
                            Maintitle = "主頁介面更改",
                            Subtitle = "首頁登入與搜尋框位置做調換"
                        },
                        new
                        {
                            Id = 8,
                            Date = "2023/07/18",
                            Maintitle = "管理員登入介面公告",
                            Subtitle = "對於新進管理人員許多人不知道隱藏登入位置，要在LINE群做公告"
                        },
                        new
                        {
                            Id = 9,
                            Date = "2023/07/19",
                            Maintitle = "購物車介面",
                            Subtitle = "購物車介面想要做更換，請聯絡開發人員做修改"
                        },
                        new
                        {
                            Id = 10,
                            Date = "2023/07/19",
                            Maintitle = "按鈕樣式",
                            Subtitle = "找尋不同按鈕以做為以後界面更新時的備選方案"
                        });
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdminlistRole", b =>
                {
                    b.Property<int>("ListId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("AdminlistId")
                        .HasColumnType("int");

                    b.Property<int?>("ToDoListViewModelsId")
                        .HasColumnType("int");

                    b.HasKey("ListId", "RoleId");

                    b.HasIndex("AdminlistId");

                    b.HasIndex("RoleId");

                    b.HasIndex("ToDoListViewModelsId");

                    b.ToTable("AdminlistRoles");

                    b.HasData(
                        new
                        {
                            ListId = 1,
                            RoleId = 4
                        },
                        new
                        {
                            ListId = 2,
                            RoleId = 4
                        },
                        new
                        {
                            ListId = 3,
                            RoleId = 4
                        },
                        new
                        {
                            ListId = 4,
                            RoleId = 4
                        },
                        new
                        {
                            ListId = 5,
                            RoleId = 4
                        },
                        new
                        {
                            ListId = 6,
                            RoleId = 4
                        },
                        new
                        {
                            ListId = 7,
                            RoleId = 4
                        },
                        new
                        {
                            ListId = 8,
                            RoleId = 4
                        },
                        new
                        {
                            ListId = 9,
                            RoleId = 4
                        },
                        new
                        {
                            ListId = 10,
                            RoleId = 4
                        });
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.Textbox", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TextValue");

                    b.HasData(
                        new
                        {
                            Id = "T001",
                            Image1 = "Deafult.jpg",
                            Image2 = "Deafult.jpg",
                            Image3 = "Deafult.jpg",
                            TextValue = "二手書交換平台"
                        });
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.ViewModels.AdministratorUserHomePage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdministratorUserHomePage");
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.ViewModels.ToDoListViewModels", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maintitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subtitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ToDoListViewModels");
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdministratorUserRoles", b =>
                {
                    b.HasOne("CoreMVC5_UsedBookProject.ViewModels.AdministratorUserHomePage", null)
                        .WithMany("AdministratorRoles")
                        .HasForeignKey("AdministratorUserHomePageId");

                    b.HasOne("CoreMVC5_UsedBookProject.Models.AdministratorRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreMVC5_UsedBookProject.Models.AdministratorUser", "User")
                        .WithMany("AdministratorRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdminlistRole", b =>
                {
                    b.HasOne("CoreMVC5_UsedBookProject.Models.Adminlist", "Adminlist")
                        .WithMany("AdminlistRoles")
                        .HasForeignKey("AdminlistId");

                    b.HasOne("CoreMVC5_UsedBookProject.Models.AdminRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreMVC5_UsedBookProject.ViewModels.ToDoListViewModels", null)
                        .WithMany("AdminlistRoles")
                        .HasForeignKey("ToDoListViewModelsId");

                    b.Navigation("Adminlist");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdministratorRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdministratorUser", b =>
                {
                    b.Navigation("AdministratorRoles");
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.Adminlist", b =>
                {
                    b.Navigation("AdminlistRoles");
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.ViewModels.AdministratorUserHomePage", b =>
                {
                    b.Navigation("AdministratorRoles");
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.ViewModels.ToDoListViewModels", b =>
                {
                    b.Navigation("AdminlistRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
