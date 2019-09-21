using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nanr.Data.Migrations
{
    public partial class billingrepurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RepurchaseAmount",
                table: "Users",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 21, 18, 9, 4, 437, DateTimeKind.Utc).AddTicks(2499));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 21, 18, 9, 4, 437, DateTimeKind.Utc).AddTicks(240));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepurchaseAmount",
                table: "Users");

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
    }
}
