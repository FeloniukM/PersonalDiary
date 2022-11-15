using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalDiary.DAL.Migrations
{
    public partial class password_lenght : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a9ca04ae-9f03-479d-900d-365326f42c66"));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "IsDelete", "Nickname", "Password", "Salt", "UpdatedAt" },
                values: new object[] { new Guid("21490871-8130-4515-b730-0b8ffd7f9c4b"), new DateTime(2022, 11, 15, 7, 41, 39, 762, DateTimeKind.Utc).AddTicks(9927), "tester@gmail.com", true, false, "admin", "Password_1", "D;%yL9TS:5PalS/d", new DateTime(2022, 11, 15, 7, 41, 39, 762, DateTimeKind.Utc).AddTicks(9928) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("21490871-8130-4515-b730-0b8ffd7f9c4b"));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "IsDelete", "Nickname", "Password", "Salt", "UpdatedAt" },
                values: new object[] { new Guid("a9ca04ae-9f03-479d-900d-365326f42c66"), new DateTime(2022, 11, 15, 7, 18, 36, 473, DateTimeKind.Utc).AddTicks(7152), "tester@gmail.com", true, false, "admin", "Password_1", "D;%yL9TS:5PalS/d", new DateTime(2022, 11, 15, 7, 18, 36, 473, DateTimeKind.Utc).AddTicks(7156) });
        }
    }
}
