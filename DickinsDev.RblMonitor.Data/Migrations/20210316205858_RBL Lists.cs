using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DickinsDev.RblMonitor.Data.Migrations
{
    public partial class RBLLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DNSBLs",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    RblName = table.Column<string>(type: "TEXT", maxLength: 24, nullable: false),
                    ZoneName = table.Column<string>(type: "TEXT", nullable: false),
                    isActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DNSBLs", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DNSBLs");
        }
    }
}
