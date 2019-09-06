using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nanr.Data.Migrations
{
    public partial class contacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 6, 21, 36, 55, 420, DateTimeKind.Utc).AddTicks(7071));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"),
                column: "CreatedOn",
                value: new DateTime(2019, 9, 6, 21, 36, 55, 420, DateTimeKind.Utc).AddTicks(6072));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("74ef2b08-6b90-46c0-bd52-2acf81f35186"),
                column: "CreatedOn",
                value: new DateTime(2019, 8, 30, 21, 52, 12, 219, DateTimeKind.Utc).AddTicks(7224));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"),
                column: "CreatedOn",
                value: new DateTime(2019, 8, 30, 21, 52, 12, 219, DateTimeKind.Utc).AddTicks(6259));
        }
    }
}
