using System.ComponentModel.DataAnnotations;

namespace RegistroUCNE.Models;

public class Registrador : Usuario
{
    [Key]
    public int RegistradorId { get; set; }

    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z0-9_-]+$",
        ErrorMessage = "El nombre de usuario solo puede contener letras, números, guiones y guiones bajos.")]
    [StringLength(30, MinimumLength = 4,
        ErrorMessage = "El nombre de usuario debe tener entre 4 y 30 caracteres.")]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(100, MinimumLength = 8,
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$",
        ErrorMessage = "La contraseña debe tener al menos una letra mayúscula, una minúscula y un número.")]
    public string Contrasena { get; set; } = string.Empty;
    public ICollection<DocumentoRegistrador> DocumentoRegistradores { get; set; } = new List<DocumentoRegistrador>();
}
