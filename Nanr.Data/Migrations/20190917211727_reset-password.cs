using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nanr.Data.Migrations
{
    public partial class resetpassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepurchaseAmount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DollarAmount",
                table: "Purchases");

            migrationBuilder.AddColumn<Guid>(
                name: "ResetCode",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "UsdAmount",
                table: "Purchases",
                type: "decimal(10, 2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NanrAmount",
                table: "Purchases",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 17, 21, 17, 27, 74, DateTimeKind.Utc).AddTicks(5601));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 17, 21, 17, 27, 74, DateTimeKind.Utc).AddTicks(4342));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NanrAmount",
                table: "Purchases");

            migrationBuilder.AddColumn<string>(
                name: "RepurchaseAmount",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UsdAmount",
                table: "Purchases",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10, 2)");

            migrationBuilder.AddColumn<decimal>(
                name: "DollarAmount",
                table: "Purchases",
                type: "decimal(10, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 16, 19, 45, 29, 58, DateTimeKind.Utc).AddTicks(5179));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 16, 19, 45, 29, 58, DateTimeKind.Utc).AddTicks(4092));
        }
    }
}
