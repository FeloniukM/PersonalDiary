using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalDiary.DAL.Migrations
{
    public partial class NewImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6eda630-5662-4334-a041-f5ceca0b4815"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "IsDelete", "Nickname", "Password", "Salt" },
                values: new object[] { new Guid("8664bfb2-4691-483b-8e38-a5dcfb28541f"), new DateTime(2022, 11, 20, 21, 32, 46, 671, DateTimeKind.Utc).AddTicks(8895), "tester@gmail.com", true, false, "admin", "Password_1", "D;%yL9TS:5PalS/d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8664bfb2-4691-483b-8e38-a5dcfb28541f"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "IsDelete", "Nickname", "Password", "Salt" },
                values: new object[] { new Guid("f6eda630-5662-4334-a041-f5ceca0b4815"), new DateTime(2022, 11, 20, 21, 29, 43, 98, DateTimeKind.Utc).AddTicks(2260), "tester@gmail.com", true, false, "admin", "Password_1", "D;%yL9TS:5PalS/d" });
        }
    }
}
