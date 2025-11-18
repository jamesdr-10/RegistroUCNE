using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroUCNE.Models;

public class Estudiante : Usuario
{
    [Key]
    public int EstudianteId { get; set; }

    [Required(ErrorMessage = "La matrícula es obligatoria.")]
    [RegularExpression(@"^\d{4}-\d{4}$",
        ErrorMessage = "La matrícula debe tener el formato 9999-9999.")]
    [StringLength(9, ErrorMessage = "La matrícula no puede superar los 9 caracteres.")]
    public string Matricula { get; set; } = string.Empty;

    [Required(ErrorMessage = "La carrera es obligatoria.")]
    [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$",
        ErrorMessage = "La carrera solo puede contener letras y espacios.")]
    [StringLength(100, ErrorMessage = "El nombre de la carrera no puede superar los 100 caracteres.")]
    public string Carrera { get; set; } = string.Empty;
    public bool EsEgresado { get; set; }

    [InverseProperty(nameof(Documento.Estudiante))]
    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
