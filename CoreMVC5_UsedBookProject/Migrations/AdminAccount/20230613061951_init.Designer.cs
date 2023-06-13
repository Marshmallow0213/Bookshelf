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
    [Migration("20230613061951_init")]
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

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = "R001",
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = "R002",
                            Name = "common user"
                        });
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdministratorUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "B001",
                            Name = "Potatodog@bookshelf.com",
                            Nickname = "馬鈴薯狗",
                            Password = "d5e10bd206b5f1aca583f37be2566e70"
                        },
                        new
                        {
                            Id = "B002",
                            Name = "Tony@bookshelf.com",
                            Nickname = "Tony",
                            Password = "213ffb90868d8b9c58aae64988f642f1"
                        },
                        new
                        {
                            Id = "B003",
                            Name = "Neverloses@bookshelf.com",
                            Nickname = "甲甲志",
                            Password = "b70d01e0fac31e93a760021e7cf970d4"
                        });
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdministratorUserRoles", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

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
                            RoleId = "R002"
                        });
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdministratorUserRoles", b =>
                {
                    b.HasOne("CoreMVC5_UsedBookProject.Models.AdminRole", "Role")
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

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdminRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CoreMVC5_UsedBookProject.Models.AdministratorUser", b =>
                {
                    b.Navigation("AdministratorRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
