using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroUCNE.Models;

public class ConfiguracionSistema
{
    [Key]
    public int ConfiguracionSistemaId { get; set; }
    public bool RequiereArchivoConHash { get; set; } = false;
    public DateTime UltimaActualizacion { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public DateTime UltimaActualizacionLocal
    {
        get => DateTime.SpecifyKind(UltimaActualizacion, DateTimeKind.Utc).ToLocalTime();
        set => UltimaActualizacion = DateTime.SpecifyKind(value, DateTimeKind.Local).ToUniversalTime();
    }
}
