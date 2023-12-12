using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressBook.DB.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressBookEntries_Departments_DepartmentsDepartmentId",
                table: "AddressBookEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressBookEntries_Jobs_JobsJobId",
                table: "AddressBookEntries");

            migrationBuilder.DropIndex(
                name: "IX_AddressBookEntries_DepartmentsDepartmentId",
                table: "AddressBookEntries");

            migrationBuilder.DropIndex(
                name: "IX_AddressBookEntries_JobsJobId",
                table: "AddressBookEntries");

            migrationBuilder.DropColumn(
                name: "DepartmentsDepartmentId",
                table: "AddressBookEntries");

            migrationBuilder.DropColumn(
                name: "JobsJobId",
                table: "AddressBookEntries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentsDepartmentId",
                table: "AddressBookEntries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobsJobId",
                table: "AddressBookEntries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddressBookEntries_DepartmentsDepartmentId",
                table: "AddressBookEntries",
                column: "DepartmentsDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressBookEntries_JobsJobId",
                table: "AddressBookEntries",
                column: "JobsJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressBookEntries_Departments_DepartmentsDepartmentId",
                table: "AddressBookEntries",
                column: "DepartmentsDepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressBookEntries_Jobs_JobsJobId",
                table: "AddressBookEntries",
                column: "JobsJobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
