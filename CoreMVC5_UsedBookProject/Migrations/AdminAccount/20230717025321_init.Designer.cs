﻿// <auto-generated />
using CoreMVC5_UsedBookProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoreMVC5_UsedBookProject.Migrations.AdminAccount
{
    [DbContext(typeof(AdminAccountContext))]
    [Migration("20230717025321_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdminRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdminRoles");

                    b.HasData(
                        new
                        {
                            Id = "R001",
                            Name = "undone"
                        },
                        new
                        {
                            Id = "R002",
                            Name = "done"
                        },
                        new
                        {
                            Id = "R003",
                            Name = "checkF"
                        },
                        new
                        {
                            Id = "R004",
                            Name = "checkT"
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
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Maintitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subtitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Adminlists");

                    b.HasData(
                        new
                        {
                            Id = "2023/07/16",
                            Maintitle = "帶烏龜看醫生",
                            subtitle = "帶烏龜到334桃園市八德區介壽路一段248-1號(丹尼爾動物醫院)看醫生，並且要確定身高體重以及服藥次數"
                        },
                        new
                        {
                            Id = "2023/07/18",
                            Maintitle = "買便當回管理員室",
                            subtitle = "因為平常樓下便當實在太難吃了，所以老闆希望可以買山下平常聚會地點的羊肉爐"
                        },
                        new
                        {
                            Id = "2023/07/20",
                            Maintitle = "倒垃圾",
                            subtitle = "管理員室的垃圾已經推積如山，所以要盡快完成"
                        });
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdminlistRole", b =>
                {
                    b.Property<string>("ListId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdminlistId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ToDoListViewModelsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ListId", "RoleId");

                    b.HasIndex("AdminlistId");

                    b.HasIndex("RoleId");

                    b.HasIndex("ToDoListViewModelsId");

                    b.ToTable("AdminlistRoles");

                    b.HasData(
                        new
                        {
                            ListId = "2023/07/16",
                            RoleId = "R001"
                        },
                        new
                        {
                            ListId = "2023/07/18",
                            RoleId = "R001"
                        },
                        new
                        {
                            ListId = "2023/07/20",
                            RoleId = "R001"
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
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Maintitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
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
