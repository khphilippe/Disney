using Microsoft.EntityFrameworkCore.Migrations;

namespace MundoDisney.Web.Migrations
{
    public partial class pp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonajePeliculas",
                table: "PersonajePeliculas");

            migrationBuilder.DropIndex(
                name: "IX_PersonajePeliculas_PeliculaId",
                table: "PersonajePeliculas");

            migrationBuilder.AlterColumn<int>(
                name: "PersonajePeliculaId",
                table: "PersonajePeliculas",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonajePeliculas",
                table: "PersonajePeliculas",
                columns: new[] { "PeliculaId", "PersonajeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonajePeliculas",
                table: "PersonajePeliculas");

            migrationBuilder.AlterColumn<int>(
                name: "PersonajePeliculaId",
                table: "PersonajePeliculas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonajePeliculas",
                table: "PersonajePeliculas",
                column: "PersonajePeliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajePeliculas_PeliculaId",
                table: "PersonajePeliculas",
                column: "PeliculaId");
        }
    }
}
