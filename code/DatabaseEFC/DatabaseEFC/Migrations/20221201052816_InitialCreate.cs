using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseEFC.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dsr-management");

            migrationBuilder.CreateTable(
                name: "Events",
                schema: "dsr-management",
                columns: table => new
                {
                    EventId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventName = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<string>(type: "text", nullable: true),
                    EndTime = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Volunteers",
                schema: "dsr-management",
                columns: table => new
                {
                    VolunteerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ShiftsTaken = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.VolunteerId);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                schema: "dsr-management",
                columns: table => new
                {
                    ManagerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VolunteerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ManagerId);
                    table.ForeignKey(
                        name: "FK_Managers_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalSchema: "dsr-management",
                        principalTable: "Volunteers",
                        principalColumn: "VolunteerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                schema: "dsr-management",
                columns: table => new
                {
                    ShiftId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VolunteerId = table.Column<long>(type: "bigint", nullable: false),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<string>(type: "text", nullable: false),
                    EndTime = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ShiftId);
                    table.ForeignKey(
                        name: "FK_Shifts_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "dsr-management",
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shifts_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalSchema: "dsr-management",
                        principalTable: "Volunteers",
                        principalColumn: "VolunteerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrators",
                schema: "dsr-management",
                columns: table => new
                {
                    AdministratorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ManagerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.AdministratorId);
                    table.ForeignKey(
                        name: "FK_Administrators_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalSchema: "dsr-management",
                        principalTable: "Managers",
                        principalColumn: "ManagerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventManager",
                schema: "dsr-management",
                columns: table => new
                {
                    EventsManagedEventId = table.Column<long>(type: "bigint", nullable: false),
                    ManagersManagerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventManager", x => new { x.EventsManagedEventId, x.ManagersManagerId });
                    table.ForeignKey(
                        name: "FK_EventManager_Events_EventsManagedEventId",
                        column: x => x.EventsManagedEventId,
                        principalSchema: "dsr-management",
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventManager_Managers_ManagersManagerId",
                        column: x => x.ManagersManagerId,
                        principalSchema: "dsr-management",
                        principalTable: "Managers",
                        principalColumn: "ManagerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_ManagerId",
                schema: "dsr-management",
                table: "Administrators",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_EventManager_ManagersManagerId",
                schema: "dsr-management",
                table: "EventManager",
                column: "ManagersManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_VolunteerId",
                schema: "dsr-management",
                table: "Managers",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_EventId",
                schema: "dsr-management",
                table: "Shifts",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_VolunteerId",
                schema: "dsr-management",
                table: "Shifts",
                column: "VolunteerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrators",
                schema: "dsr-management");

            migrationBuilder.DropTable(
                name: "EventManager",
                schema: "dsr-management");

            migrationBuilder.DropTable(
                name: "Shifts",
                schema: "dsr-management");

            migrationBuilder.DropTable(
                name: "Managers",
                schema: "dsr-management");

            migrationBuilder.DropTable(
                name: "Events",
                schema: "dsr-management");

            migrationBuilder.DropTable(
                name: "Volunteers",
                schema: "dsr-management");
        }
    }
}
