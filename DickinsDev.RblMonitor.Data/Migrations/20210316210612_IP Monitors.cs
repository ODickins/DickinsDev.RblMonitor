using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DickinsDev.RblMonitor.Data.Migrations
{
    public partial class IPMonitors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IPMonitors",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    IPName = table.Column<string>(type: "TEXT", maxLength: 24, nullable: false),
                    IPAddress = table.Column<string>(type: "TEXT", nullable: false),
                    isActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastCheck = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInterval = table.Column<int>(type: "INTEGER", nullable: false),
                    isClean = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPMonitors", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPMonitors");
        }
    }
}
