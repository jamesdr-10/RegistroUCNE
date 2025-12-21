using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroUCNE.Models;

public class ConfiguracionSistema
{
    [Key]
    public int ConfiguracionSistemaId { get; set; }
    
    public bool RequiereArchivoConHash { get; set; } = true;
    public bool ReciboPagoObligatorio { get; set; } = true;
    public string DocumentosFavoritosRaw { get; set; } = "";
    public DateTime UltimaActualizacion { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public List<int> DocumentosFavoritos
    {
        get
        {
            if (string.IsNullOrWhiteSpace(DocumentosFavoritosRaw))
                return new List<int>();

            return DocumentosFavoritosRaw
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }
        set
        {
            DocumentosFavoritosRaw = string.Join(",", value);
        }
    }

    [NotMapped]
    public DateTime UltimaActualizacionLocal
    {
        get => DateTime.SpecifyKind(UltimaActualizacion, DateTimeKind.Utc).ToLocalTime();
        set => UltimaActualizacion = DateTime.SpecifyKind(value, DateTimeKind.Local).ToUniversalTime();
    }
}
