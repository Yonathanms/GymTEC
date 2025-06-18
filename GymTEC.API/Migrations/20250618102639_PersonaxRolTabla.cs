using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTEC.API.Migrations
{
    /// <inheritdoc />
    public partial class PersonaxRolTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonaxRol",
                columns: table => new
                {
                    NumCedula = table.Column<string>(type: "text", nullable: false),
                    IdRol = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaxRol", x => new { x.NumCedula, x.IdRol });
                    table.ForeignKey(
                        name: "FK_PersonaxRol_Personas_NumCedula",
                        column: x => x.NumCedula,
                        principalTable: "Personas",
                        principalColumn: "NumCedula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaxRol_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonaxRol_IdRol",
                table: "PersonaxRol",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonaxRol");
        }
    }
}
