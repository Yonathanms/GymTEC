using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GymTEC.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    NumCedula = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Apellido1 = table.Column<string>(type: "text", nullable: false),
                    Apellido2 = table.Column<string>(type: "text", nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Provincia = table.Column<string>(type: "text", nullable: false),
                    Canton = table.Column<string>(type: "text", nullable: false),
                    Distrito = table.Column<string>(type: "text", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.NumCedula);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreRol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Sucursal",
                columns: table => new
                {
                    IdSucursal = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreSucursal = table.Column<string>(type: "text", nullable: false),
                    Provincia = table.Column<string>(type: "text", nullable: false),
                    Canton = table.Column<string>(type: "text", nullable: false),
                    Distrito = table.Column<string>(type: "text", nullable: false),
                    OpeningDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    MaxCapacity = table.Column<int>(type: "integer", nullable: false),
                    SpaEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    StoreEnabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.IdSucursal);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    NumCedula = table.Column<string>(type: "text", nullable: false),
                    Peso = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    IMC = table.Column<decimal>(type: "numeric(4,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.NumCedula);
                    table.ForeignKey(
                        name: "FK_Cliente_Personas_NumCedula",
                        column: x => x.NumCedula,
                        principalTable: "Personas",
                        principalColumn: "NumCedula",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    NumCedula = table.Column<string>(type: "text", nullable: false),
                    IdSucursal = table.Column<int>(type: "integer", nullable: true),
                    TipoPlanilla = table.Column<string>(type: "text", nullable: false),
                    Salario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.NumCedula);
                    table.ForeignKey(
                        name: "FK_Administrador_Personas_NumCedula",
                        column: x => x.NumCedula,
                        principalTable: "Personas",
                        principalColumn: "NumCedula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Administrador_Sucursal_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "IdSucursal");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_IdSucursal",
                table: "Administrador",
                column: "IdSucursal",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonaxRol_IdRol",
                table: "PersonaxRol",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "PersonaxRol");

            migrationBuilder.DropTable(
                name: "Sucursal");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
