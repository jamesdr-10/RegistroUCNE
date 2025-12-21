using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroUCNE.Models;

public class SolicitudDocumento
{
    [Key]
    public int SolicitudDocumentoId { get; set; }
    public int TipoDocumentoId { get; set; }
    public int EstudianteId { get; set; }

    public string EstadoProceso { get; set; } = "Iniciado";
    public string? DescripcionAdicional {get; set;}

    public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;
    public DateTime? FechaFinalizado { get; set; }
    public DateTime? FechaEntrega { get; set; }

    public bool Activo { get; set; } = true;
    
    public ICollection<SolicitudDocumentoRegistrador> TrabajosRegistrador { get; set; } = [];

    public Documento? DocumentoFinal { get; set; }

    [ForeignKey(nameof(EstudianteId))]
    public Estudiante? Estudiante { get; set; }

    [ForeignKey(nameof(TipoDocumentoId))]
    public TipoDocumento? TipoDocumento { get; set; }
    
    [NotMapped]
    public DateTime FechaSolicitudLocal
    {
        get => DateTime.SpecifyKind(FechaSolicitud, DateTimeKind.Utc).ToLocalTime();
        set => FechaSolicitud = DateTime.SpecifyKind(value, DateTimeKind.Local).ToUniversalTime();
    }

    [NotMapped]
    public DateTime? FechaFinalizadoLocal
    {
        get => FechaFinalizado.HasValue
            ? DateTime.SpecifyKind(FechaFinalizado.Value, DateTimeKind.Utc).ToLocalTime()
            : null;
        set => FechaFinalizado = value.HasValue
            ? DateTime.SpecifyKind(value.Value, DateTimeKind.Local).ToUniversalTime()
            : null;
    }

    [NotMapped]
    public DateTime? FechaEntregaLocal
    {
        get => FechaEntrega.HasValue
            ? DateTime.SpecifyKind(FechaEntrega.Value, DateTimeKind.Utc).ToLocalTime()
            : null;
        set => FechaEntrega = value.HasValue
            ? DateTime.SpecifyKind(value.Value, DateTimeKind.Local).ToUniversalTime()
            : null;
    }
}
