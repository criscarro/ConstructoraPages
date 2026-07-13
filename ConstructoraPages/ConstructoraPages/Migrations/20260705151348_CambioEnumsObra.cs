using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstructoraPages.Migrations
{
    /// <inheritdoc />
    public partial class CambioEnumsObra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha_Fin",
                table: "Obra");

            migrationBuilder.DropColumn(
                name: "Fecha_Inicio",
                table: "Obra");

            migrationBuilder.DropColumn(
                name: "Tipo_Obra",
                table: "Obra");

            migrationBuilder.RenameColumn(
                name: "Costo_Actual",
                table: "Obra",
                newName: "CostoActual");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "Obra",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFinal",
                table: "Obra",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicio",
                table: "Obra",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TipoObra",
                table: "Obra",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaFinal",
                table: "Obra");

            migrationBuilder.DropColumn(
                name: "FechaInicio",
                table: "Obra");

            migrationBuilder.DropColumn(
                name: "TipoObra",
                table: "Obra");

            migrationBuilder.RenameColumn(
                name: "CostoActual",
                table: "Obra",
                newName: "Costo_Actual");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Obra",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Fecha_Fin",
                table: "Obra",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Fecha_Inicio",
                table: "Obra",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Tipo_Obra",
                table: "Obra",
                type: "longtext",
                nullable: false);
        }
    }
}
