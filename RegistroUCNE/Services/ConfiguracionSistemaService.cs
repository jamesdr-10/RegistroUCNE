using Microsoft.EntityFrameworkCore;
using RegistroUCNE.DAL;
using RegistroUCNE.Models;

namespace RegistroUCNE.Services;

public class ConfiguracionSistemaService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<ConfiguracionSistema> Obtener()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var config = await contexto.ConfiguracionesSistema.FirstOrDefaultAsync();

        if (config == null)
        {
            config = new ConfiguracionSistema
            {
                ConfiguracionSistemaId = 1,
                RequiereArchivoConHash = false,
                UltimaActualizacion = DateTime.UtcNow
            };

            contexto.ConfiguracionesSistema.Add(config);
            await contexto.SaveChangesAsync();
        }

        return config;
    }

    public async Task Guardar(ConfiguracionSistema config)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(config);
        await contexto.SaveChangesAsync();
    }
}
