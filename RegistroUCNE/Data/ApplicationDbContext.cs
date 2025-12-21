using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RegistroUCNE.Models;

namespace RegistroUCNE.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<SolicitudDocumento> SolicitudesDocumentos { get; set; }
    public DbSet<Documento> Documentos { get; set; }
    public DbSet<Estudiante> Estudiantes { get; set; }
    public DbSet<TipoDocumento> TiposDocumentos { get; set; }
    public DbSet<SolicitudDocumentoRegistrador> SolicitudesDocumentosRegistradores { get; set; }
    public DbSet<ConfiguracionSistema> ConfiguracionesSistema { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var roles = new List<IdentityRole<int>>
        {
            new IdentityRole<int> { Id = 1, Name = "Administrador", NormalizedName = "ADMINISTRADOR", ConcurrencyStamp = "admin-stamp" },
            new IdentityRole<int> { Id = 2, Name = "Registrador", NormalizedName = "REGISTRADOR", ConcurrencyStamp = "registrador-stamp" },
        };

        builder.Entity<IdentityRole<int>>().HasData(roles);

        var adminUser = new ApplicationUser
        {
            Id = 1,
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@ucne.edu.do",
            NormalizedEmail = "ADMIN@UCNE.EDU.DO",
            PasswordHash = "AQAAAAIAAYagAAAAEMnRieJLtO0k/Mv75oJw3AWMP/CFhqba42LJNnLiGlLXCmqmXFJ+MM9flIMz4PXa9g==",
            SecurityStamp = "admin-security-stamp",
            ConcurrencyStamp = "admin-concurrency-stamp",

            Nombres = "Administrador",
            Apellidos = "Del Sistema",
            Direccion = "San Francisco de Macorís",
        };

        builder.Entity<ApplicationUser>().HasData(adminUser);

        builder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int>
            {
                UserId = 1,
                RoleId = 1
            }
        );

        builder.Entity<SolicitudDocumento>()
            .HasOne(s => s.DocumentoFinal)
            .WithOne(d => d.SolicitudDocumento)
            .HasForeignKey<Documento>(d => d.SolicitudDocumentoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<TipoDocumento>().HasData(
            new TipoDocumento { TipoDocumentoId = 1, Nombre = "Certificación de Estudiante" },
            new TipoDocumento { TipoDocumentoId = 2, Nombre = "Certificación con Requerimientos por Casos Especifícos" },
            new TipoDocumento { TipoDocumentoId = 3, Nombre = "Certificación de Grado" },
            new TipoDocumento { TipoDocumentoId = 4, Nombre = "Certificación de Última Materia" },
            new TipoDocumento { TipoDocumentoId = 5, Nombre = "Certificación de Internado Rotatorio" },
            new TipoDocumento { TipoDocumentoId = 6, Nombre = "Certificación de Convenio" },
            new TipoDocumento { TipoDocumentoId = 7, Nombre = "Certificación de Monográfico" },
            new TipoDocumento { TipoDocumentoId = 8, Nombre = "Certificación de Anillo" },
            new TipoDocumento { TipoDocumentoId = 9, Nombre = "Revisión de Expediente y Récord de Notas Regular" },
            new TipoDocumento { TipoDocumentoId = 10, Nombre = "Récord de Notas Egresado" },
            new TipoDocumento { TipoDocumentoId = 11, Nombre = "Récord de Notas Manuales de Matrículas Antiguas" },
            new TipoDocumento { TipoDocumentoId = 12, Nombre = "Documentos Académicos para Legalización del MESCyT" },
            new TipoDocumento { TipoDocumentoId = 13, Nombre = "Documentos Académicos con Homologaciones para Legalización del MESCyT" },
            new TipoDocumento { TipoDocumentoId = 14, Nombre = "Documentos para Validación de Estudios en Colombia" },
            new TipoDocumento { TipoDocumentoId = 15, Nombre = "Otro (Especificar)" }
        );
    }
}
