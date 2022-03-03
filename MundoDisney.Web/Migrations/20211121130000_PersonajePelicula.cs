using Microsoft.EntityFrameworkCore.Migrations;

namespace MundoDisney.Web.Migrations
{
    public partial class PersonajePelicula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_Generos_GeneroId",
                table: "Peliculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_Personajes_PersonajeId",
                table: "Peliculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Qualification_Peliculas_PeliculaId",
                table: "Qualification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personajes",
                table: "Personajes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Peliculas",
                table: "Peliculas");

            migrationBuilder.DropIndex(
                name: "IX_Peliculas_PersonajeId",
                table: "Peliculas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Generos",
                table: "Generos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Personajes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "PersonajeId",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Generos");

            migrationBuilder.AddColumn<int>(
                name: "PersonajeId",
                table: "Personajes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PeliculaId",
                table: "Peliculas",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "Generos",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personajes",
                table: "Personajes",
                column: "PersonajeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Peliculas",
                table: "Peliculas",
                column: "PeliculaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Generos",
                table: "Generos",
                column: "GeneroId");

            migrationBuilder.CreateTable(
                name: "PersonajePeliculas",
                columns: table => new
                {
                    PersonajePeliculaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonajeId = table.Column<int>(nullable: false),
                    PeliculaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajePeliculas", x => x.PersonajePeliculaId);
                    table.ForeignKey(
                        name: "FK_PersonajePeliculas_Peliculas_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "PeliculaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajePeliculas_Personajes_PersonajeId",
                        column: x => x.PersonajeId,
                        principalTable: "Personajes",
                        principalColumn: "PersonajeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonajePeliculas_PeliculaId",
                table: "PersonajePeliculas",
                column: "PeliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajePeliculas_PersonajeId",
                table: "PersonajePeliculas",
                column: "PersonajeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_Generos_GeneroId",
                table: "Peliculas",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "GeneroId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Qualification_Peliculas_PeliculaId",
                table: "Qualification",
                column: "PeliculaId",
                principalTable: "Peliculas",
                principalColumn: "PeliculaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_Generos_GeneroId",
                table: "Peliculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Qualification_Peliculas_PeliculaId",
                table: "Qualification");

            migrationBuilder.DropTable(
                name: "PersonajePeliculas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personajes",
                table: "Personajes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Peliculas",
                table: "Peliculas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Generos",
                table: "Generos");

            migrationBuilder.DropColumn(
                name: "PersonajeId",
                table: "Personajes");

            migrationBuilder.DropColumn(
                name: "PeliculaId",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "Generos");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Personajes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Peliculas",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PersonajeId",
                table: "Peliculas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Generos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personajes",
                table: "Personajes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Peliculas",
                table: "Peliculas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Generos",
                table: "Generos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_PersonajeId",
                table: "Peliculas",
                column: "PersonajeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_Generos_GeneroId",
                table: "Peliculas",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_Personajes_PersonajeId",
                table: "Peliculas",
                column: "PersonajeId",
                principalTable: "Personajes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Qualification_Peliculas_PeliculaId",
                table: "Qualification",
                column: "PeliculaId",
                principalTable: "Peliculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
