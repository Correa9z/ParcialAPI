using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcialAPI.Migrations
{
    /// <inheritdoc />
    public partial class girardot_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    useDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isUsed = table.Column<bool>(type: "bit", nullable: false),
                    entranceGate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_id",
                table: "Tickets",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
