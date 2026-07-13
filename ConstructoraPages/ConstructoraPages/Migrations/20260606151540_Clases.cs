using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ConstructoraPages.Migrations
{
    /// <inheritdoc />
    public partial class Clases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Presupuesto",
                table: "Obra",
                type: "double",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<double>(
                name: "Costo_Actual",
                table: "Obra",
                type: "double",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Apellido = table.Column<string>(type: "longtext", nullable: false),
                    Cedula = table.Column<string>(type: "longtext", nullable: false),
                    Telefono = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contratista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Cedula = table.Column<string>(type: "longtext", nullable: false),
                    Telefono = table.Column<string>(type: "longtext", nullable: false),
                    Especialidad = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratista", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Cedula = table.Column<string>(type: "longtext", nullable: false),
                    Telefono = table.Column<string>(type: "longtext", nullable: false),
                    Cargo = table.Column<string>(type: "longtext", nullable: false),
                    Salario = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EtapaObra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false),
                    Estado = table.Column<string>(type: "longtext", nullable: false),
                    FechaInicio = table.Column<string>(type: "longtext", nullable: false),
                    FechaFin = table.Column<string>(type: "longtext", nullable: false),
                    IdObra = table.Column<int>(type: "int", nullable: false),
                    ObraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapaObra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtapaObra_Obra_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Gasto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false),
                    Monto = table.Column<double>(type: "double", nullable: false),
                    Fecha = table.Column<string>(type: "longtext", nullable: false),
                    Tipo = table.Column<string>(type: "longtext", nullable: false),
                    IdObra = table.Column<int>(type: "int", nullable: false),
                    ObraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gasto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gasto_Obra_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false),
                    Precio = table.Column<double>(type: "double", nullable: false),
                    stock = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Direccion = table.Column<string>(type: "longtext", nullable: false),
                    Telefono = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClienteObra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    IdObra = table.Column<int>(type: "int", nullable: false),
                    ObraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteObra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteObra_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteObra_Obra_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ContratistaObra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FechaInicio = table.Column<string>(type: "longtext", nullable: false),
                    FechaFin = table.Column<string>(type: "longtext", nullable: false),
                    CostoPorTrabajo = table.Column<double>(type: "double", nullable: false),
                    IdContratista = table.Column<int>(type: "int", nullable: false),
                    ContratistaId = table.Column<int>(type: "int", nullable: false),
                    IdObra = table.Column<int>(type: "int", nullable: false),
                    ObraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratistaObra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContratistaObra_Contratista_ContratistaId",
                        column: x => x.ContratistaId,
                        principalTable: "Contratista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratistaObra_Obra_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AsignacionEmpleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    IdObra = table.Column<int>(type: "int", nullable: false),
                    ObraId = table.Column<int>(type: "int", nullable: false),
                    FechaAsignacion = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionEmpleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsignacionEmpleado_Empleado_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignacionEmpleado_Obra_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CompraMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MontoTotal = table.Column<double>(type: "double", nullable: false),
                    Fecha = table.Column<string>(type: "longtext", nullable: false),
                    IdObra = table.Column<int>(type: "int", nullable: false),
                    ObraId = table.Column<int>(type: "int", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraMaterial_Obra_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraMaterial_Proveedor_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DetalleCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<string>(type: "longtext", nullable: false),
                    Precio = table.Column<double>(type: "double", nullable: false),
                    IdCompra = table.Column<int>(type: "int", nullable: false),
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    IdMaterial = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_CompraMaterial_CompraId",
                        column: x => x.CompraId,
                        principalTable: "CompraMaterial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionEmpleado_EmpleadoId",
                table: "AsignacionEmpleado",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionEmpleado_ObraId",
                table: "AsignacionEmpleado",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteObra_ClienteId",
                table: "ClienteObra",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteObra_ObraId",
                table: "ClienteObra",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraMaterial_ObraId",
                table: "CompraMaterial",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraMaterial_ProveedorId",
                table: "CompraMaterial",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratistaObra_ContratistaId",
                table: "ContratistaObra",
                column: "ContratistaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratistaObra_ObraId",
                table: "ContratistaObra",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_CompraId",
                table: "DetalleCompra",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_MaterialId",
                table: "DetalleCompra",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_EtapaObra_ObraId",
                table: "EtapaObra",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_Gasto_ObraId",
                table: "Gasto",
                column: "ObraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsignacionEmpleado");

            migrationBuilder.DropTable(
                name: "ClienteObra");

            migrationBuilder.DropTable(
                name: "ContratistaObra");

            migrationBuilder.DropTable(
                name: "DetalleCompra");

            migrationBuilder.DropTable(
                name: "EtapaObra");

            migrationBuilder.DropTable(
                name: "Gasto");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Contratista");

            migrationBuilder.DropTable(
                name: "CompraMaterial");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.AlterColumn<string>(
                name: "Presupuesto",
                table: "Obra",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<string>(
                name: "Costo_Actual",
                table: "Obra",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }
    }
}
