using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalDiary.DAL.Migrations
{
    public partial class compression : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5d84d296-e3c5-49e8-8c7b-5238950e42d5"));

            migrationBuilder.AddColumn<bool>(
                name: "IsCompressed",
                table: "Records",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "IsDelete", "Nickname", "Password", "Salt", "UpdatedAt" },
                values: new object[] { new Guid("7169d52f-3a68-4739-ac0f-f88b95511554"), new DateTime(2022, 11, 19, 9, 28, 14, 255, DateTimeKind.Utc).AddTicks(1797), "tester@gmail.com", true, false, "admin", "Password_1", "D;%yL9TS:5PalS/d", new DateTime(2022, 11, 19, 9, 28, 14, 255, DateTimeKind.Utc).AddTicks(1800) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7169d52f-3a68-4739-ac0f-f88b95511554"));

            migrationBuilder.DropColumn(
                name: "IsCompressed",
                table: "Records");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "IsDelete", "Nickname", "Password", "Salt", "UpdatedAt" },
                values: new object[] { new Guid("5d84d296-e3c5-49e8-8c7b-5238950e42d5"), new DateTime(2022, 11, 17, 20, 19, 1, 926, DateTimeKind.Utc).AddTicks(5113), "tester@gmail.com", true, false, "admin", "Password_1", "D;%yL9TS:5PalS/d", new DateTime(2022, 11, 17, 20, 19, 1, 926, DateTimeKind.Utc).AddTicks(5115) });
        }
    }
}
