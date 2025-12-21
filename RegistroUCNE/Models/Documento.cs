using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RegistroUCNE.Data;

namespace RegistroUCNE.Models;

public class Documento
{
    [Key]
    public int DocumentoId { get; set; }
    public int SolicitudDocumentoId { get; set; }
    public int SubidoPorUsuarioId { get; set; }

    [Required]
    public string NombreOriginal { get; set; } = string.Empty;

    [Required]
    public string RutaR2 { get; set; } = string.Empty;

    [Required]
    public string HashSha256 { get; set; } = string.Empty;

    [Required]
    public string TokenAcceso { get; set; } = Guid.NewGuid().ToString("N");

    public DateTime FechaSubida { get; set; } = DateTime.UtcNow;

    public long TamanoBytes { get; set; }
    public string ContentType { get; set; } = "application/pdf";

    public bool Activo { get; set; } = true;

    [ForeignKey(nameof(SolicitudDocumentoId))]
    public SolicitudDocumento? SolicitudDocumento { get; set; }

    [ForeignKey(nameof(SubidoPorUsuarioId))]
    public ApplicationUser? SubidoPor { get; set; }
}
