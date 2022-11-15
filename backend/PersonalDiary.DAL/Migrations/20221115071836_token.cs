using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalDiary.DAL.Migrations
{
    public partial class token : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4d3ded5-2597-4fec-9ba0-40a1f99f778a"));

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "IsDelete", "Nickname", "Password", "Salt", "UpdatedAt" },
                values: new object[] { new Guid("a9ca04ae-9f03-479d-900d-365326f42c66"), new DateTime(2022, 11, 15, 7, 18, 36, 473, DateTimeKind.Utc).AddTicks(7152), "tester@gmail.com", true, false, "admin", "Password_1", "D;%yL9TS:5PalS/d", new DateTime(2022, 11, 15, 7, 18, 36, 473, DateTimeKind.Utc).AddTicks(7156) });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a9ca04ae-9f03-479d-900d-365326f42c66"));

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "IsDelete", "Nickname", "Password", "UpdatedAt" },
                values: new object[] { new Guid("c4d3ded5-2597-4fec-9ba0-40a1f99f778a"), new DateTime(2022, 11, 14, 19, 30, 58, 266, DateTimeKind.Utc).AddTicks(7084), "tester@gmail.com", true, false, "admin", "Password_1", new DateTime(2022, 11, 14, 19, 30, 58, 266, DateTimeKind.Utc).AddTicks(7085) });
        }
    }
}
