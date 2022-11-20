using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalDiary.DAL.Migrations
{
    public partial class improve_image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("32fe4f78-3cfd-49b6-ba53-69679d8aa9e0"));

            migrationBuilder.RenameColumn(
                name: "ThumbUrl",
                table: "Images",
                newName: "Thumb_url");

            migrationBuilder.RenameColumn(
                name: "PermalinkUrl",
                table: "Images",
                newName: "Permalink_url");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Images",
                newName: "Image_id");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "IsDelete", "Nickname", "Password", "Salt" },
                values: new object[] { new Guid("dd81b70a-edb2-41ff-b223-51a05e5d6d05"), new DateTime(2022, 11, 20, 17, 6, 59, 426, DateTimeKind.Utc).AddTicks(3032), "tester@gmail.com", true, false, "admin", "Password_1", "D;%yL9TS:5PalS/d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dd81b70a-edb2-41ff-b223-51a05e5d6d05"));

            migrationBuilder.RenameColumn(
                name: "Thumb_url",
                table: "Images",
                newName: "ThumbUrl");

            migrationBuilder.RenameColumn(
                name: "Permalink_url",
                table: "Images",
                newName: "PermalinkUrl");

            migrationBuilder.RenameColumn(
                name: "Image_id",
                table: "Images",
                newName: "FileId");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "IsDelete", "Nickname", "Password", "Salt" },
                values: new object[] { new Guid("32fe4f78-3cfd-49b6-ba53-69679d8aa9e0"), new DateTime(2022, 11, 20, 16, 31, 59, 752, DateTimeKind.Utc).AddTicks(201), "tester@gmail.com", true, false, "admin", "Password_1", "D;%yL9TS:5PalS/d" });
        }
    }
}
