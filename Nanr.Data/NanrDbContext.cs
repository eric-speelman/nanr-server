using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Nanr.Data.Models;
using System;

namespace Nanr.Data
{
    public class NanrDbContext : DbContext
    {
        public NanrDbContext(DbContextOptions<NanrDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(x => x.Email);
            builder.Entity<User>().HasIndex(x => x.Username);
            builder.Entity<Click>().HasOne(x => x.User).WithMany(x => x.Clicks).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<User>().HasMany(x => x.Withdraws).WithOne(x => x.User);
            builder.Entity<User>().HasMany(x => x.ReferreeWithdraws).WithOne(x => x.Referral);
            builder.Entity<Withdraw>()
            .Property(x => x.UsdAmount)
            .HasColumnType("decimal(10, 2)");
            builder.Entity<Purchase>()
            .Property(x => x.UsdAmount)
            .HasColumnType("decimal(10, 2)");
            var userId = Guid.Parse("8352b38f-7be1-4497-8b66-e9776d2ab8f1");
            var tagId = Guid.Parse("c748c8e3-da3f-4151-9e0d-190d1923c5ac");
            var testUserId = Guid.Parse("74ef2b08-6b90-46c0-bd52-2acf81f35186");
            var testTagId = Guid.Parse("fa81e3d2-5741-46ab-875e-5e6a14870eb0");
            builder.Entity<User>().HasData(
                new User
                {
                    Id = userId,
                    Email = "eric.t.speelman@gmail.com",
                    Username = "Eric",
                    Salt = "RfJSCsZNibfFN7+d19Cy8A==",
                    Balance = 20,
                    PasswordHash = "2jAJXn2ZLlH3oewf9tAb0Sl6ushDB0unLNqsRv3TBcw=",
                    CreatedOn = DateTime.UtcNow,
                    LastLogin = null,
                    Tagline = "Little things add up",
                    BackgroundColor = "#FAFAFA",
                    isStandTextDark = true
                },
                new User
                {
                    Id = testUserId,
                    Email = "test@fake.com",
                    Username = "test",
                    Salt = "RfJSCsZNibfFN7+d19Cy8A==",
                    Balance = 20,
                    PasswordHash = "2jAJXn2ZLlH3oewf9tAb0Sl6ushDB0unLNqsRv3TBcw=",
                    CreatedOn = DateTime.UtcNow,
                    LastLogin = null,
                    Tagline = "Little things add up",
                    BackgroundColor = "#FAFAFA",
                    isStandTextDark = true
                }
            );
            builder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = tagId,
                    UserId = userId,
                    IsDefault = true
                },
                new Tag
                {
                    Id = testTagId,
                    UserId = testUserId,
                    IsDefault = true
                });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Click> Clicks { get; set; }
        public DbSet<Withdraw> Withdraws { get; set; }
        public DbSet<TagView> TagViews { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
}
