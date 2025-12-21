using Microsoft.EntityFrameworkCore;
using RegistroUCNE.Data;
using RegistroUCNE.Models;

namespace RegistroUCNE.Services;

public class ConfiguracionSistemaService(IDbContextFactory<ApplicationDbContext> DbFactory)
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
            };

            contexto.ConfiguracionesSistema.Add(config);
            await contexto.SaveChangesAsync();
        }

        if (config.DocumentosFavoritosRaw == null)
            config.DocumentosFavoritosRaw = "";

        return config;
    }

    public async Task Guardar(ConfiguracionSistema config)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(config);
        await contexto.SaveChangesAsync();
    }
}
