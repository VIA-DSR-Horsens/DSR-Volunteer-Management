using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseEFC.Migrations
{
    public partial class ExtendingAdministrator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ManagerId",
                schema: "dsr-management",
                table: "Administrators",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_ManagerId",
                schema: "dsr-management",
                table: "Administrators",
                column: "ManagerId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrators_Managers_ManagerId",
                schema: "dsr-management",
                table: "Administrators");

            migrationBuilder.DropIndex(
                name: "IX_Administrators_ManagerId",
                schema: "dsr-management",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                schema: "dsr-management",
                table: "Administrators");
        }
    }
}
