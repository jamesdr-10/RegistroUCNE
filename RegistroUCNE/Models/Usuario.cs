using System.ComponentModel.DataAnnotations;

namespace RegistroUCNE.Models;

public class Usuario
{
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$",
        ErrorMessage = "El nombre solo puede contener letras y espacios.")]
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$",
        ErrorMessage = "El apellido solo puede contener letras y espacios.")]
    [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [RegularExpression(@"^[A-Za-z0-9ÁÉÍÓÚáéíóúÑñ#\.,\-\s]+$",
        ErrorMessage = "La dirección contiene caracteres inválidos.")]
    [StringLength(150, ErrorMessage = "La dirección no puede superar los 150 caracteres.")]
    public string Direccion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
    [StringLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres.")]
    public string Correo { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
}
