using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalDiary.DAL.Migrations
{
    public partial class image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("21490871-8130-4515-b730-0b8ffd7f9c4b"));

            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Records",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "IsDelete", "Nickname", "Password", "Salt", "UpdatedAt" },
                values: new object[] { new Guid("5d84d296-e3c5-49e8-8c7b-5238950e42d5"), new DateTime(2022, 11, 17, 20, 19, 1, 926, DateTimeKind.Utc).AddTicks(5113), "tester@gmail.com", true, false, "admin", "Password_1", "D;%yL9TS:5PalS/d", new DateTime(2022, 11, 17, 20, 19, 1, 926, DateTimeKind.Utc).AddTicks(5115) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5d84d296-e3c5-49e8-8c7b-5238950e42d5"));

            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Records");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "IsDelete", "Nickname", "Password", "Salt", "UpdatedAt" },
                values: new object[] { new Guid("21490871-8130-4515-b730-0b8ffd7f9c4b"), new DateTime(2022, 11, 15, 7, 41, 39, 762, DateTimeKind.Utc).AddTicks(9927), "tester@gmail.com", true, false, "admin", "Password_1", "D;%yL9TS:5PalS/d", new DateTime(2022, 11, 15, 7, 41, 39, 762, DateTimeKind.Utc).AddTicks(9928) });
        }
    }
}
