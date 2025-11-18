using Microsoft.EntityFrameworkCore;
using RegistroUCNE.Models;

namespace RegistroUCNE.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions options) : base(options) { }

    public DbSet<Documento> Documentos { get; set; }
    public DbSet<Estudiante> Estudiantes { get; set; }
    public DbSet<Registrador> Registradores { get; set; }
    public DbSet<TipoDocumento> TipoDocumentos { get; set; }
    public DbSet<DocumentoRegistrador> DocumentoRegistradores { get; set; }
    public DbSet<ConfiguracionSistema> ConfiguracionesSistema { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Registrador>().HasData(
                new Registrador
                {
                    RegistradorId = 1,
                    Nombres = "Administrador",
                    Apellidos = "Del Sistema",
                    Direccion = "San Francisco de Macorís",
                    Correo = "admin@ucne.edu.do",
                    NombreUsuario = "admin",
                    Contrasena = "$2a$11$WgzaECgDTNz.bkhft1.0ju8CiPq6uD1ILAznXmsm19DgHW6REa0Rq",
                }
            );

        modelBuilder.Entity<TipoDocumento>().HasData(
            new List<TipoDocumento>()
            {
                new() { TipoDocumentoId = 1, Nombre = "Certificación de Estudiante" },
                new() { TipoDocumentoId = 2, Nombre = "Certificación con Requerimientos por Casos Especifícos" },
                new() { TipoDocumentoId = 3, Nombre = "Certificación de Grado" },
                new() { TipoDocumentoId = 4, Nombre = "Certificación de Última Materia" },
                new() { TipoDocumentoId = 5, Nombre = "Certificación de Internado Rotatorio" },
                new() { TipoDocumentoId = 6, Nombre = "Certificación de Convenio" },
                new() { TipoDocumentoId = 7, Nombre = "Certificación de Monográfico" },
                new() { TipoDocumentoId = 8, Nombre = "Certificación de Anillo" },
                new() { TipoDocumentoId = 9, Nombre = "Revisión de Expediente y Récord de Notas Regular" },
                new() { TipoDocumentoId = 10, Nombre = "Récord de Notas Egresado" },
                new() { TipoDocumentoId = 11, Nombre = "Récord de Notas Manuales de Matrículas Antiguas" },
                new() { TipoDocumentoId = 12, Nombre = "Documentos Académicos para Legalización del MESCyT" },
                new() { TipoDocumentoId = 13, Nombre = "Documentos Académicos con Homologaciones para Legalización del MESCyT" },
                new() { TipoDocumentoId = 14, Nombre = "Documentos para Validación de Estudios en Colombia" },
                new() { TipoDocumentoId = 15, Nombre = "Otro (Especificar)" }
            }
        );
    }
}
