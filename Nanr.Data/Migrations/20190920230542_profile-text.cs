using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nanr.Data.Migrations
{
    public partial class profiletext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isStandTextDark",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186"),
                columns: new[] { "BackgroundColor", "CreatedOn", "Tagline", "isStandTextDark" },
                values: new object[] { "#FAFAFA", new DateTime(2019, 9, 20, 23, 5, 41, 744, DateTimeKind.Utc).AddTicks(2276), "Little things add up", true });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"),
                columns: new[] { "BackgroundColor", "CreatedOn", "Tagline", "isStandTextDark" },
                values: new object[] { "#FAFAFA", new DateTime(2019, 9, 20, 23, 5, 41, 744, DateTimeKind.Utc).AddTicks(77), "Little things add up", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isStandTextDark",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186"),
                columns: new[] { "BackgroundColor", "CreatedOn", "Tagline" },
                values: new object[] { null, new DateTime(2019, 9, 20, 20, 56, 10, 333, DateTimeKind.Utc).AddTicks(5049), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"),
                columns: new[] { "BackgroundColor", "CreatedOn", "Tagline" },
                values: new object[] { null, new DateTime(2019, 9, 20, 20, 56, 10, 333, DateTimeKind.Utc).AddTicks(3857), null });
        }
    }
}
