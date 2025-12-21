using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroUCNE.Models;

public class TipoDocumento
{
    [Key]
    public int TipoDocumentoId { get; set; }

    [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
    [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El tipo de documento solo puede contener letras y espacios.")]
    [StringLength(100, ErrorMessage = "El nombre del tipo de documento no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [InverseProperty(nameof(SolicitudDocumento.TipoDocumento))]
    public ICollection<SolicitudDocumento> SolicitudDocumentos { get; set; } = [];
}
