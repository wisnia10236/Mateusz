using Microsoft.EntityFrameworkCore.Migrations;

namespace randka.Migrations
{
    public partial class InitialCreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "values",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_values", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "values");
        }

        // Add-Migration InitialCreateDB    <-zainicjowanie db  w package manager
        // Update-database   <- aby zakualizowac badz utworzyc baze rzeczywista
    }
}
