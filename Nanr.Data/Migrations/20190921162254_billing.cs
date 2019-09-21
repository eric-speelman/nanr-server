using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nanr.Data.Migrations
{
    public partial class billing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillingId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Repurchase",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 21, 16, 22, 54, 245, DateTimeKind.Utc).AddTicks(552));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 21, 16, 22, 54, 244, DateTimeKind.Utc).AddTicks(7693));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Repurchase",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 20, 23, 5, 41, 744, DateTimeKind.Utc).AddTicks(2276));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 20, 23, 5, 41, 744, DateTimeKind.Utc).AddTicks(77));
        }
    }
}
