using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressBook.DB.Migrations
{
    public partial class UpdateAddPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MobileNumber",
                table: "AddressBookEntries",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "AddressBookEntries",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoFileName",
                table: "AddressBookEntries",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "AddressBookEntries");

            migrationBuilder.DropColumn(
                name: "PhotoFileName",
                table: "AddressBookEntries");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "AddressBookEntries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
