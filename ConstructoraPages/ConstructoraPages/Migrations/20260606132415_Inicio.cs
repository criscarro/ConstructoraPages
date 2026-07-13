using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ConstructoraPages.Migrations
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Obra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Direccion = table.Column<string>(type: "longtext", nullable: false),
                    Tipo_Obra = table.Column<string>(type: "longtext", nullable: false),
                    Estado = table.Column<string>(type: "longtext", nullable: false),
                    Fecha_Inicio = table.Column<string>(type: "longtext", nullable: false),
                    Fecha_Fin = table.Column<string>(type: "longtext", nullable: false),
                    Presupuesto = table.Column<string>(type: "longtext", nullable: false),
                    Costo_Actual = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obra", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Obra");
        }
    }
}
