using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ConstructoraPages.Migrations
{
    /// <inheritdoc />
    public partial class AgregarEntidadesRestantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienteObra_Cliente_ClienteId",
                table: "ClienteObra");

            migrationBuilder.DropForeignKey(
                name: "FK_ClienteObra_Obra_ObraId",
                table: "ClienteObra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClienteObra",
                table: "ClienteObra");

            migrationBuilder.DropIndex(
                name: "IX_ClienteObra_ClienteId",
                table: "ClienteObra");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClienteObra");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "ClienteObra");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "ClienteObra");

            migrationBuilder.RenameColumn(
                name: "stock",
                table: "Material",
                newName: "Stock");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Material",
                newName: "PrecioUnitario");

            migrationBuilder.RenameColumn(
                name: "ObraId",
                table: "ClienteObra",
                newName: "ObrasId");

            migrationBuilder.RenameColumn(
                name: "IdObra",
                table: "ClienteObra",
                newName: "ClientesId");

            migrationBuilder.RenameIndex(
                name: "IX_ClienteObra_ObraId",
                table: "ClienteObra",
                newName: "IX_ClienteObra_ObrasId");

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "Material",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "Gasto",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "Gasto",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "EtapaObra",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "EtapaObra",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "EtapaObra",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<int>(
                name: "Cargo",
                table: "Empleado",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "DetalleCompra",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "ContratistaObra",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "ContratistaObra",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<int>(
                name: "Especialidad",
                table: "Contratista",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "CompraMaterial",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAsignacion",
                table: "AsignacionEmpleado",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienteObra",
                table: "ClienteObra",
                columns: new[] { "ClientesId", "ObrasId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteObra_Cliente_ClientesId",
                table: "ClienteObra",
                column: "ClientesId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteObra_Obra_ObrasId",
                table: "ClienteObra",
                column: "ObrasId",
                principalTable: "Obra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienteObra_Cliente_ClientesId",
                table: "ClienteObra");

            migrationBuilder.DropForeignKey(
                name: "FK_ClienteObra_Obra_ObrasId",
                table: "ClienteObra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClienteObra",
                table: "ClienteObra");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Material",
                newName: "stock");

            migrationBuilder.RenameColumn(
                name: "PrecioUnitario",
                table: "Material",
                newName: "Precio");

            migrationBuilder.RenameColumn(
                name: "ObrasId",
                table: "ClienteObra",
                newName: "ObraId");

            migrationBuilder.RenameColumn(
                name: "ClientesId",
                table: "ClienteObra",
                newName: "IdObra");

            migrationBuilder.RenameIndex(
                name: "IX_ClienteObra_ObrasId",
                table: "ClienteObra",
                newName: "IX_ClienteObra_ObraId");

            migrationBuilder.AlterColumn<string>(
                name: "stock",
                table: "Material",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Gasto",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Fecha",
                table: "Gasto",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "FechaInicio",
                table: "EtapaObra",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "FechaFin",
                table: "EtapaObra",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "EtapaObra",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "Empleado",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Cantidad",
                table: "DetalleCompra",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "FechaInicio",
                table: "ContratistaObra",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "FechaFin",
                table: "ContratistaObra",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Especialidad",
                table: "Contratista",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Fecha",
                table: "CompraMaterial",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ClienteObra",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "ClienteObra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "ClienteObra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "FechaAsignacion",
                table: "AsignacionEmpleado",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienteObra",
                table: "ClienteObra",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteObra_ClienteId",
                table: "ClienteObra",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteObra_Cliente_ClienteId",
                table: "ClienteObra",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteObra_Obra_ObraId",
                table: "ClienteObra",
                column: "ObraId",
                principalTable: "Obra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
