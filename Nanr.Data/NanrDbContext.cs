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
            builder.Entity<Click>().HasOne(x => x.User).WithMany(x => x.Clicks).OnDelete(DeleteBehavior.Restrict);
            var userId = Guid.Parse("8352b38f-7be1-4497-8b66-e9776d2ab8f1");
            var tagId = Guid.Parse("c748c8e3-da3f-4151-9e0d-190d1923c5ac");
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
                }
            );
            builder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = tagId,
                    UserId = userId,
                    IsDefault = true
                }); ;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Click> Clicks { get; set; }
        public DbSet<Withdraw> Withdraws { get; set; }
    }
}
