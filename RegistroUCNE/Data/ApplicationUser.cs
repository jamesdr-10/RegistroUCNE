using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RegistroUCNE.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser<int>
{
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        public string Apellidos { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [RegularExpression(@"^[A-Za-z0-9ÁÉÍÓÚáéíóúÑñ#\.,\-\s]+$", ErrorMessage = "La dirección contiene caracteres inválidos.")]
        [StringLength(150, ErrorMessage = "La dirección no puede superar los 150 caracteres.")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        [StringLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres.")]
        public override string? Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^(?:\d{3}-\d{3}-\d{4}|\d{10})$", ErrorMessage = "El teléfono debe tener el formato 000-000-0000")]
        [StringLength(12, ErrorMessage = "El teléfono no puede superar los 12 caracteres.")]
        public override string? PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [RegularExpression(@"^[A-Za-z0-9_-]{4,30}$", ErrorMessage = "Solo letras, números, - y _. Entre 4 y 30 caracteres.")]
        public override string? UserName { get; set; } = "";
}

