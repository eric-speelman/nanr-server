﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nanr.Data;

namespace Nanr.Data.Migrations
{
    [DbContext(typeof(NanrDbContext))]
    partial class NanrDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("PageId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Referrer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ViewId")
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

            modelBuilder.Entity("Nanr.Data.Models.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NanrAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("UsdAmount")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id");

                    b.ToTable("Purchases");
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

                    b.Property<string>("PageId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Referrer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

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

                    b.Property<string>("BackgroundColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<string>("BillingId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("EmailConfirmationCode")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ReferrerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RefferrerRemainder")
                        .HasColumnType("int");

                    b.Property<bool>("Repurchase")
                        .HasColumnType("bit");

                    b.Property<string>("RepurchaseAmount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ResetCode")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tagline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isStandTextDark")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("ReferrerId");

                    b.HasIndex("Username");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"),
                            BackgroundColor = "#FAFAFA",
                            Balance = 20,
                            CreatedOn = new DateTime(2019, 9, 22, 13, 44, 26, 812, DateTimeKind.Utc).AddTicks(6159),
                            Email = "eric.t.speelman@gmail.com",
                            PasswordHash = "2jAJXn2ZLlH3oewf9tAb0Sl6ushDB0unLNqsRv3TBcw=",
                            RefferrerRemainder = 0,
                            Repurchase = false,
                            Salt = "RfJSCsZNibfFN7+d19Cy8A==",
                            Tagline = "Little things add up",
                            Username = "Eric",
                            isStandTextDark = true
                        },
                        new
                        {
                            Id = new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186"),
                            BackgroundColor = "#FAFAFA",
                            Balance = 20,
                            CreatedOn = new DateTime(2019, 9, 22, 13, 44, 26, 812, DateTimeKind.Utc).AddTicks(8397),
                            Email = "test@fake.com",
                            PasswordHash = "2jAJXn2ZLlH3oewf9tAb0Sl6ushDB0unLNqsRv3TBcw=",
                            RefferrerRemainder = 0,
                            Repurchase = false,
                            Salt = "RfJSCsZNibfFN7+d19Cy8A==",
                            Tagline = "Little things add up",
                            Username = "test",
                            isStandTextDark = true
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

                    b.Property<Guid?>("ReferralId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RefferalAmount")
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

                    b.HasIndex("ReferralId");

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

            modelBuilder.Entity("Nanr.Data.Models.User", b =>
                {
                    b.HasOne("Nanr.Data.Models.User", "Referrer")
                        .WithMany()
                        .HasForeignKey("ReferrerId");
                });

            modelBuilder.Entity("Nanr.Data.Models.Withdraw", b =>
                {
                    b.HasOne("Nanr.Data.Models.User", "Referral")
                        .WithMany("ReferreeWithdraws")
                        .HasForeignKey("ReferralId");

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
