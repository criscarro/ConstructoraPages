using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstructoraPages.Migrations
{
    /// <inheritdoc />
    public partial class ReemplazarFksPorPropiedadesDeNavegacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdObra",
                table: "Gasto");

            migrationBuilder.DropColumn(
                name: "IdObra",
                table: "EtapaObra");

            migrationBuilder.DropColumn(
                name: "IdCompra",
                table: "DetalleCompra");

            migrationBuilder.DropColumn(
                name: "IdMaterial",
                table: "DetalleCompra");

            migrationBuilder.DropColumn(
                name: "IdContratista",
                table: "ContratistaObra");

            migrationBuilder.DropColumn(
                name: "IdObra",
                table: "ContratistaObra");

            migrationBuilder.DropColumn(
                name: "IdObra",
                table: "CompraMaterial");

            migrationBuilder.DropColumn(
                name: "IdProveedor",
                table: "CompraMaterial");

            migrationBuilder.DropColumn(
                name: "IdEmpleado",
                table: "AsignacionEmpleado");

            migrationBuilder.DropColumn(
                name: "IdObra",
                table: "AsignacionEmpleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdObra",
                table: "Gasto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdObra",
                table: "EtapaObra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCompra",
                table: "DetalleCompra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdMaterial",
                table: "DetalleCompra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdContratista",
                table: "ContratistaObra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdObra",
                table: "ContratistaObra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdObra",
                table: "CompraMaterial",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdProveedor",
                table: "CompraMaterial",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpleado",
                table: "AsignacionEmpleado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdObra",
                table: "AsignacionEmpleado",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
