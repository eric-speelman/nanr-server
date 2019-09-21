using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nanr.Data.Migrations
{
    public partial class @ref : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReferrerId",
                table: "Users",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_ReferrerId",
                table: "Users",
                column: "ReferrerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ReferrerId",
                table: "Users",
                column: "ReferrerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ReferrerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ReferrerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReferrerId",
                table: "Users");

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
    }
}
