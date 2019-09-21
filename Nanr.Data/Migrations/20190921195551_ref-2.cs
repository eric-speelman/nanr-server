using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nanr.Data.Migrations
{
    public partial class ref2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReferralId",
                table: "Withdraws",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RefferalAmount",
                table: "Withdraws",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RefferrerRemainder",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 21, 19, 55, 51, 475, DateTimeKind.Utc).AddTicks(7809));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 21, 19, 55, 51, 475, DateTimeKind.Utc).AddTicks(5455));

            migrationBuilder.CreateIndex(
                name: "IX_Withdraws_ReferralId",
                table: "Withdraws",
                column: "ReferralId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Withdraws_Users_ReferralId",
                table: "Withdraws",
                column: "ReferralId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Withdraws_Users_ReferralId",
                table: "Withdraws");

            migrationBuilder.DropIndex(
                name: "IX_Withdraws_ReferralId",
                table: "Withdraws");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReferralId",
                table: "Withdraws");

            migrationBuilder.DropColumn(
                name: "RefferalAmount",
                table: "Withdraws");

            migrationBuilder.DropColumn(
                name: "RefferrerRemainder",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 21, 19, 15, 50, 463, DateTimeKind.Utc).AddTicks(2257));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 21, 19, 15, 50, 463, DateTimeKind.Utc).AddTicks(24));
        }
    }
}
