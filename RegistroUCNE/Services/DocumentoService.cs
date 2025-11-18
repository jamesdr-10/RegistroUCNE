using Microsoft.EntityFrameworkCore;
using RegistroUCNE.DAL;
using RegistroUCNE.Models;
using System.Linq.Expressions;

namespace RegistroUCNE.Services;

public class DocumentoService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int documentoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Documentos.AnyAsync(d => d.DocumentoId == documentoId);
    }

    private async Task<bool> Insertar(Documento documento)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Documentos.Add(documento);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Documento documento)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Documentos.Update(documento);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Documento documento)
    {
        if (!await Existe(documento.DocumentoId))
        {
            return await Insertar(documento);
        } else
        {
            return await Modificar(documento);
        }
    }

    public async Task<bool> Deshabilitar(int documentoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var documento = await contexto.Documentos
            .Include(d => d.DocumentoRegistradores)
            .Include(e => e.Estudiante)
            .Include(t => t.TipoDocumento)
            .FirstOrDefaultAsync(d => d.DocumentoId == documentoId);

        if (documento == null)
        {
            return false;
        }

        documento.Activo = false;
        return await contexto.SaveChangesAsync() > 0;
    }

     public async Task<bool> Habilitar(int documentoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var documento = await contexto.Documentos
            .Include(d => d.Estudiante)
            .FirstOrDefaultAsync(d => d.DocumentoId == documentoId);

        if (documento == null)
            return false;

        documento.Activo = true;

        if (documento.Estudiante != null && !documento.Estudiante.Activo)
            documento.Estudiante.Activo = true;

        await contexto.SaveChangesAsync();
        return true;
    }

    public async Task<Documento?> Buscar(int documentoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Documentos
            .Include(r => r.DocumentoRegistradores)
            .Include(e => e.Estudiante)
            .Include(t => t.TipoDocumento)
            .FirstOrDefaultAsync(d => d.DocumentoId == documentoId);
    }

    public async Task<List<Documento>> Listar(Expression<Func<Documento, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Documentos
            .Where(criterio)
            .Include(d => d.DocumentoRegistradores)
            .Include(d => d.TipoDocumento)
            .Include(d => d.Estudiante)
            .OrderByDescending(d => d.FechaSolicitud)
            .ToListAsync();
    }
}
