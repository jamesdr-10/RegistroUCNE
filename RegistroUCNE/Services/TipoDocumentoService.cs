using Microsoft.EntityFrameworkCore;
using RegistroUCNE.Data;
using RegistroUCNE.Models;

namespace RegistroUCNE.Services;

public class TipoDocumentoService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    public async Task<List<TipoDocumento>> Listar()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposDocumentos
            .Include(s => s.SolicitudDocumentos)
            .OrderBy(t => t.TipoDocumentoId)
            .ToListAsync();
    }
}
