using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RegistroUCNE.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracionesSistema",
                columns: table => new
                {
                    ConfiguracionSistemaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequiereArchivoConHash = table.Column<bool>(type: "boolean", nullable: false),
                    UltimaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionesSistema", x => x.ConfiguracionSistemaId);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    EstudianteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Matricula = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    Carrera = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EsEgresado = table.Column<bool>(type: "boolean", nullable: false),
                    Nombres = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Correo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.EstudianteId);
                });

            migrationBuilder.CreateTable(
                name: "Registradores",
                columns: table => new
                {
                    RegistradorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreUsuario = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Contrasena = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Nombres = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Correo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registradores", x => x.RegistradorId);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumentos",
                columns: table => new
                {
                    TipoDocumentoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumentos", x => x.TipoDocumentoId);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    DocumentoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipoDocumentoId = table.Column<int>(type: "integer", nullable: false),
                    EstudianteId = table.Column<int>(type: "integer", nullable: false),
                    EstadoProceso = table.Column<string>(type: "text", nullable: false),
                    DescripcionAdicional = table.Column<string>(type: "text", nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFinalizado = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaEntrega = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ArchivoPdfRuta = table.Column<string>(type: "text", nullable: true),
                    HashArchivo = table.Column<string>(type: "text", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.DocumentoId);
                    table.ForeignKey(
                        name: "FK_Documentos_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documentos_TipoDocumentos_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumentos",
                        principalColumn: "TipoDocumentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentoRegistradores",
                columns: table => new
                {
                    DocumentoRegistradorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DocumentoId = table.Column<int>(type: "integer", nullable: false),
                    RegistradorId = table.Column<int>(type: "integer", nullable: false),
                    FechaTrabajo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DescripcionTrabajo = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentoRegistradores", x => x.DocumentoRegistradorId);
                    table.ForeignKey(
                        name: "FK_DocumentoRegistradores_Documentos_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documentos",
                        principalColumn: "DocumentoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentoRegistradores_Registradores_RegistradorId",
                        column: x => x.RegistradorId,
                        principalTable: "Registradores",
                        principalColumn: "RegistradorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Registradores",
                columns: new[] { "RegistradorId", "Activo", "Apellidos", "Contrasena", "Correo", "Direccion", "NombreUsuario", "Nombres" },
                values: new object[] { 1, true, "Del Sistema", "$2a$11$WgzaECgDTNz.bkhft1.0ju8CiPq6uD1ILAznXmsm19DgHW6REa0Rq", "admin@ucne.edu.do", "San Francisco de Macorís", "admin", "Administrador" });

            migrationBuilder.InsertData(
                table: "TipoDocumentos",
                columns: new[] { "TipoDocumentoId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Certificación de Estudiante" },
                    { 2, "Certificación con Requerimientos por Casos Especifícos" },
                    { 3, "Certificación de Grado" },
                    { 4, "Certificación de Última Materia" },
                    { 5, "Certificación de Internado Rotatorio" },
                    { 6, "Certificación de Convenio" },
                    { 7, "Certificación de Monográfico" },
                    { 8, "Certificación de Anillo" },
                    { 9, "Revisión de Expediente y Récord de Notas Regular" },
                    { 10, "Récord de Notas Egresado" },
                    { 11, "Récord de Notas Manuales de Matrículas Antiguas" },
                    { 12, "Documentos Académicos para Legalización del MESCyT" },
                    { 13, "Documentos Académicos con Homologaciones para Legalización del MESCyT" },
                    { 14, "Documentos para Validación de Estudios en Colombia" },
                    { 15, "Otro (Especificar)" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoRegistradores_DocumentoId",
                table: "DocumentoRegistradores",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoRegistradores_RegistradorId",
                table: "DocumentoRegistradores",
                column: "RegistradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_EstudianteId",
                table: "Documentos",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_TipoDocumentoId",
                table: "Documentos",
                column: "TipoDocumentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracionesSistema");

            migrationBuilder.DropTable(
                name: "DocumentoRegistradores");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Registradores");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "TipoDocumentos");
        }
    }
}
