﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nanr.Data;

namespace Nanr.Data.Migrations
{
    [DbContext(typeof(NanrDbContext))]
    [Migration("20190909233503_withdraw-email")]
    partial class withdrawemail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview9.19423.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nanr.Data.Models.Click", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Page")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("UserId");

                    b.ToTable("Clicks");
                });

            modelBuilder.Entity("Nanr.Data.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Nanr.Data.Models.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Nanr.Data.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c748c8e3-da3f-4151-9e0d-190d1923c5ac"),
                            IsDefault = true,
                            UserId = new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1")
                        },
                        new
                        {
                            Id = new Guid("fa81e3d2-5741-46ab-875e-5e6a14870eb0"),
                            IsDefault = true,
                            UserId = new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186")
                        });
                });

            modelBuilder.Entity("Nanr.Data.Models.TagView", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Page")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("UserId");

                    b.ToTable("TagViews");
                });

            modelBuilder.Entity("Nanr.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepurchaseAmount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"),
                            Balance = 20,
                            CreatedOn = new DateTime(2019, 9, 9, 23, 35, 2, 746, DateTimeKind.Utc).AddTicks(3779),
                            Email = "eric.t.speelman@gmail.com",
                            PasswordHash = "2jAJXn2ZLlH3oewf9tAb0Sl6ushDB0unLNqsRv3TBcw=",
                            Salt = "RfJSCsZNibfFN7+d19Cy8A==",
                            Username = "Eric"
                        },
                        new
                        {
                            Id = new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186"),
                            Balance = 20,
                            CreatedOn = new DateTime(2019, 9, 9, 23, 35, 2, 746, DateTimeKind.Utc).AddTicks(6700),
                            Email = "test@fake.com",
                            PasswordHash = "2jAJXn2ZLlH3oewf9tAb0Sl6ushDB0unLNqsRv3TBcw=",
                            Salt = "RfJSCsZNibfFN7+d19Cy8A==",
                            Username = "test"
                        });
                });

            modelBuilder.Entity("Nanr.Data.Models.Withdraw", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NanrAmount")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TransactionFee")
                        .HasColumnType("int");

                    b.Property<decimal>("UsdAmount")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Withdraws");
                });

            modelBuilder.Entity("Nanr.Data.Models.Click", b =>
                {
                    b.HasOne("Nanr.Data.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nanr.Data.Models.User", "User")
                        .WithMany("Clicks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Nanr.Data.Models.Session", b =>
                {
                    b.HasOne("Nanr.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nanr.Data.Models.Tag", b =>
                {
                    b.HasOne("Nanr.Data.Models.User", "User")
                        .WithMany("Tags")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nanr.Data.Models.TagView", b =>
                {
                    b.HasOne("Nanr.Data.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nanr.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Nanr.Data.Models.Withdraw", b =>
                {
                    b.HasOne("Nanr.Data.Models.User", "User")
                        .WithMany("Withdraws")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
