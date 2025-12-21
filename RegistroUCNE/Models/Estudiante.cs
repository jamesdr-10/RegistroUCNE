using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroUCNE.Models;

public class Estudiante
{
    [Key]
    public int EstudianteId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
    [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "La matrícula es obligatoria.")]
    [RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "La matrícula debe tener el formato 0000-0000.")]
    [StringLength(9, ErrorMessage = "La matrícula no puede superar los 9 caracteres.")]
    public string Matricula { get; set; } = string.Empty;

    [Required(ErrorMessage = "La carrera es obligatoria.")]
    [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "La carrera solo puede contener letras y espacios.")]
    [StringLength(100, ErrorMessage = "El nombre de la carrera no puede superar los 100 caracteres.")]
    public string Carrera { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "El teléfono debe tener el formato 000-000-0000")]
    [StringLength(12, ErrorMessage = "El teléfono no puede superar los 12 caracteres.")]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
    [StringLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres.")]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [RegularExpression(@"^[A-Za-z0-9ÁÉÍÓÚáéíóúÑñ#\.,\-\s]+$", ErrorMessage = "La dirección contiene caracteres inválidos.")]
    [StringLength(150, ErrorMessage = "La dirección no puede superar los 150 caracteres.")]
    public string Direccion { get; set; } = string.Empty;

    public bool EsEgresado { get; set; }
    public bool Activo { get; set; } = true;

    [InverseProperty(nameof(SolicitudDocumento.Estudiante))]
    public virtual ICollection<SolicitudDocumento> SolicitudDocumentos { get; set; } = [];
}
