using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseEFC.Migrations
{
    public partial class FinishingDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrators_Managers_ManagerId",
                schema: "dsr-management",
                table: "Administrators");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                schema: "dsr-management",
                table: "Administrators",
                newName: "VolunteerId");

            migrationBuilder.RenameIndex(
                name: "IX_Administrators_ManagerId",
                schema: "dsr-management",
                table: "Administrators",
                newName: "IX_Administrators_VolunteerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrators_Volunteers_VolunteerId",
                schema: "dsr-management",
                table: "Administrators",
                column: "VolunteerId",
                principalSchema: "dsr-management",
                principalTable: "Volunteers",
                principalColumn: "VolunteerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrators_Volunteers_VolunteerId",
                schema: "dsr-management",
                table: "Administrators");

            migrationBuilder.RenameColumn(
                name: "VolunteerId",
                schema: "dsr-management",
                table: "Administrators",
                newName: "ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Administrators_VolunteerId",
                schema: "dsr-management",
                table: "Administrators",
                newName: "IX_Administrators_ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrators_Managers_ManagerId",
                schema: "dsr-management",
                table: "Administrators",
                column: "ManagerId",
                principalSchema: "dsr-management",
                principalTable: "Managers",
                principalColumn: "ManagerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
