using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTEC.API.Migrations
{
    /// <inheritdoc />
    public partial class TablaClientev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Personas_NumCedula",
                table: "Cliente",
                column: "NumCedula",
                principalTable: "Personas",
                principalColumn: "NumCedula",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Personas_NumCedula",
                table: "Cliente");
        }
    }
}
