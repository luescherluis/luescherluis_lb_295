using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballManagement.Migrations
{
    /// <inheritdoc />
    public partial class DbCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fussballmannschaft",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fussballmannschaft", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spieler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nachname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vorname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spieler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FussballmannschaftSpieler",
                columns: table => new
                {
                    FussballmannschaftId = table.Column<int>(type: "int", nullable: false),
                    SpielerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FussballmannschaftSpieler", x => new { x.FussballmannschaftId, x.SpielerId });
                    table.ForeignKey(
                        name: "FK_FussballmannschaftSpieler_Fussballmannschaft_FussballmannschaftId",
                        column: x => x.FussballmannschaftId,
                        principalTable: "Fussballmannschaft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FussballmannschaftSpieler_Spieler_SpielerId",
                        column: x => x.SpielerId,
                        principalTable: "Spieler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FussballmannschaftSpieler_SpielerId",
                table: "FussballmannschaftSpieler",
                column: "SpielerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FussballmannschaftSpieler");

            migrationBuilder.DropTable(
                name: "Fussballmannschaft");

            migrationBuilder.DropTable(
                name: "Spieler");
        }
    }
}
