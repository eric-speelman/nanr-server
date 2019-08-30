using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nanr.Data.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    Balance = table.Column<int>(nullable: false),
                    RepurchaseAmount = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Withdraws",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    NanrAmount = table.Column<int>(nullable: false),
                    UsdAmount = table.Column<decimal>(nullable: false),
                    TransactionFee = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdraws", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Withdraws_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clicks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false),
                    Page = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clicks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clicks_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clicks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "CreatedOn", "Email", "LastLogin", "PasswordHash", "RepurchaseAmount", "Salt", "Username" },
                values: new object[] { new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1"), 20, new DateTime(2019, 8, 27, 21, 24, 14, 160, DateTimeKind.Utc).AddTicks(7766), "eric.t.speelman@gmail.com", null, "2jAJXn2ZLlH3oewf9tAb0Sl6ushDB0unLNqsRv3TBcw=", null, "RfJSCsZNibfFN7+d19Cy8A==", "Eric" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "CreatedOn", "Email", "LastLogin", "PasswordHash", "RepurchaseAmount", "Salt", "Username" },
                values: new object[] { new Guid("91b44665-e0a8-418d-9344-175ff2404025"), 20, new DateTime(2019, 8, 27, 21, 24, 14, 160, DateTimeKind.Utc).AddTicks(8727), "test@gmail.com", null, "2jAJXn2ZLlH3oewf9tAb0Sl6ushDB0unLNqsRv3TBcw=", null, "RfJSCsZNibfFN7+d19Cy8A==", "Password" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsDefault", "UserId" },
                values: new object[] { new Guid("7fa3c34f-8488-471d-b293-7dd43c977396"), true, new Guid("8352b38f-7be1-4497-8b66-e9776d2ab8f1") });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsDefault", "UserId" },
                values: new object[] { new Guid("c748c8e3-da3f-4151-9e0d-190d1923c5ac"), true, new Guid("91b44665-e0a8-418d-9344-175ff2404025") });

            migrationBuilder.CreateIndex(
                name: "IX_Clicks_TagId",
                table: "Clicks",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Clicks_UserId",
                table: "Clicks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UserId",
                table: "Tags",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Withdraws_UserId",
                table: "Withdraws",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clicks");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Withdraws");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
