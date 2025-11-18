using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroUCNE.Models;

public class Documento
{
    [Key]
    public int DocumentoId { get; set; }
    public int TipoDocumentoId { get; set; }
    public int EstudianteId { get; set; }
    public string EstadoProceso { get; set; } = "Iniciado";
    public string? DescripcionAdicional {get; set;}
    public DateTime FechaSolicitud { get; set; }
    public DateTime? FechaFinalizado { get; set; }
    public DateTime? FechaEntrega { get; set; }
    public string? ArchivoPdfRuta { get; set; }
    public string? HashArchivo { get; set; }
    public bool Activo { get; set; } = true;
    public ICollection<DocumentoRegistrador> DocumentoRegistradores { get; set; } = new List<DocumentoRegistrador>();

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
