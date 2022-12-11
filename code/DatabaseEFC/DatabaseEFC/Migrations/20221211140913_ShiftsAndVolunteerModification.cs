using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseEFC.Migrations
{
    public partial class ShiftsAndVolunteerModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                schema: "dsr-management",
                table: "Shifts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                schema: "dsr-management",
                table: "Shifts");
        }
    }
}
