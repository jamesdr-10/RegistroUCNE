using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RegistroUCNE.Data;

namespace RegistroUCNE.Models;

public class SolicitudDocumentoRegistrador
{
    [Key]
    public int SolicitudDocumentoRegistradorId { get; set; }
    public int SolicitudDocumentoId { get; set; }
    public int RegistradorId { get; set; }
    
    public DateTime FechaTrabajo { get; set; } = DateTime.UtcNow;

    [MaxLength(300)]
    public string? DescripcionTrabajo { get; set; }

    [ForeignKey(nameof(SolicitudDocumentoId))]
    public SolicitudDocumento? SolicitudDocumento { get; set; }

    [ForeignKey(nameof(RegistradorId))]
    public ApplicationUser? Registrador { get; set; }

    [NotMapped]
    public DateTime FechaTrabajoLocal
    {
        get => DateTime.SpecifyKind(FechaTrabajo, DateTimeKind.Utc).ToLocalTime();
        set => FechaTrabajo = DateTime.SpecifyKind(value, DateTimeKind.Local).ToUniversalTime();
    }
}
