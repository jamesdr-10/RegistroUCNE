using Microsoft.EntityFrameworkCore;
using RegistroUCNE.DAL;
using RegistroUCNE.Models;

namespace RegistroUCNE.Services;

public class TipoDocumentoService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int tipoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TipoDocumentos.AnyAsync(t => t.TipoDocumentoId == tipoId);
    }

    private async Task<bool> Insertar(TipoDocumento tipo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.TipoDocumentos.Add(tipo);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(TipoDocumento tipo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.TipoDocumentos.Update(tipo);
        return await contexto.SaveChangesAsync() > 0; 
    }

    public async Task<bool> Guardar(TipoDocumento tipo)
    {
        if (!await Existe(tipo.TipoDocumentoId))
        {
            return await Insertar(tipo);
        } else
        {
            return await Modificar(tipo);
        }
    }

    public async Task<bool> Eliminar(int tipoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TipoDocumentos.Where(t => t.TipoDocumentoId == tipoId).ExecuteDeleteAsync() > 0;
    }

    public async Task<TipoDocumento?> Buscar(int tipoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TipoDocumentos
            .Include(d => d.Documentos)
            .FirstOrDefaultAsync(t => t.TipoDocumentoId == tipoId);
    }

    public async Task<List<TipoDocumento>> Listar()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TipoDocumentos
            .Include(d => d.Documentos)
            .OrderBy(t => t.TipoDocumentoId)
            .ToListAsync();
    }
}
