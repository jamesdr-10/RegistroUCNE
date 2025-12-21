using Microsoft.EntityFrameworkCore;
using RegistroUCNE.Data;
using RegistroUCNE.Models;
using System.Linq.Expressions;

namespace RegistroUCNE.Services;

public class SolicitudDocumentoService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    private async Task<bool> Existe(int documentoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SolicitudesDocumentos.AnyAsync(sd => sd.SolicitudDocumentoId == documentoId);
    }

    private async Task<bool> Insertar(SolicitudDocumento solicitudDocumento)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.SolicitudesDocumentos.Add(solicitudDocumento);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(SolicitudDocumento solicitudDocumento)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var existente = await contexto.SolicitudesDocumentos
            .Include(s => s.DocumentoFinal)
            .Include(s => s.TrabajosRegistrador)
            .FirstOrDefaultAsync(s => s.SolicitudDocumentoId == solicitudDocumento.SolicitudDocumentoId);

        if (existente == null)
            return false;

        existente.EstadoProceso = solicitudDocumento.EstadoProceso;
        existente.FechaSolicitud = solicitudDocumento.FechaSolicitud;
        existente.FechaFinalizado = solicitudDocumento.FechaFinalizado;
        existente.FechaEntrega = solicitudDocumento.FechaEntrega;
        existente.DescripcionAdicional = solicitudDocumento.DescripcionAdicional;
        existente.Activo = solicitudDocumento.Activo;

        if (solicitudDocumento.DocumentoFinal != null)
        {
            if (existente.DocumentoFinal == null)
            {
                existente.DocumentoFinal = solicitudDocumento.DocumentoFinal;
            }
            else
            {
                existente.DocumentoFinal.RutaR2 = solicitudDocumento.DocumentoFinal.RutaR2;
                existente.DocumentoFinal.HashSha256 = solicitudDocumento.DocumentoFinal.HashSha256;
                existente.DocumentoFinal.NombreOriginal = solicitudDocumento.DocumentoFinal.NombreOriginal;
                existente.DocumentoFinal.TamanoBytes = solicitudDocumento.DocumentoFinal.TamanoBytes;
                existente.DocumentoFinal.ContentType = solicitudDocumento.DocumentoFinal.ContentType;
            }
        }

        var nuevoTrabajo = solicitudDocumento.TrabajosRegistrador.FirstOrDefault(t => t.SolicitudDocumentoRegistradorId == 0);

        if (nuevoTrabajo != null)
        {
            existente.TrabajosRegistrador.Add(new SolicitudDocumentoRegistrador
            {
                RegistradorId = nuevoTrabajo.RegistradorId,
                FechaTrabajo = nuevoTrabajo.FechaTrabajo,
                DescripcionTrabajo = nuevoTrabajo.DescripcionTrabajo
            });
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(SolicitudDocumento solicitudDocumento)
    {
        if (!await Existe(solicitudDocumento.SolicitudDocumentoId))
        {
            return await Insertar(solicitudDocumento);
        } else
        {
            return await Modificar(solicitudDocumento);
        }
    }

    public async Task<bool> Deshabilitar(int solicitudDocumentoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var solicitudDocumento = await contexto.SolicitudesDocumentos
            .Include(s => s.DocumentoFinal)
            .FirstOrDefaultAsync(s => s.SolicitudDocumentoId == solicitudDocumentoId);

        if (solicitudDocumento == null)
            return false;

        solicitudDocumento.Activo = false;

        if (solicitudDocumento.DocumentoFinal != null)
        {
            solicitudDocumento.DocumentoFinal.Activo = false;
            solicitudDocumento.DocumentoFinal.TokenAcceso = Guid.NewGuid().ToString("N");
        }

        return await contexto.SaveChangesAsync() > 0;
    }

     public async Task<bool> Habilitar(int solicitudDocumentoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var solicitudDocumento = await contexto.SolicitudesDocumentos
            .Include(s => s.DocumentoFinal)
            .Include(s => s.TrabajosRegistrador)
            .Include(e => e.Estudiante)
            .FirstOrDefaultAsync(s => s.SolicitudDocumentoId == solicitudDocumentoId);

        if (solicitudDocumento == null)
            return false;

        solicitudDocumento.Activo = true;

        // Reactivar documento y generar NUEVO token
        if (solicitudDocumento.DocumentoFinal != null)
        {
            solicitudDocumento.DocumentoFinal.Activo = true;
            solicitudDocumento.DocumentoFinal.TokenAcceso = Guid.NewGuid().ToString("N");
        }

        if (solicitudDocumento.Estudiante != null && !solicitudDocumento.Estudiante.Activo)
            solicitudDocumento.Estudiante.Activo = true;

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<SolicitudDocumento?> Buscar(int solicitudDocumentoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SolicitudesDocumentos
            .Include(s => s.DocumentoFinal)
            .Include(r => r.TrabajosRegistrador)
                .ThenInclude(r => r.Registrador)
            .Include(e => e.Estudiante)
            .Include(t => t.TipoDocumento)
            .FirstOrDefaultAsync(s => s.SolicitudDocumentoId == solicitudDocumentoId);
    }

    public async Task<List<SolicitudDocumento>> Listar(Expression<Func<SolicitudDocumento, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.SolicitudesDocumentos
            .Where(criterio)
            .Include(s => s.DocumentoFinal)
            .Include(r => r.TrabajosRegistrador)
                .ThenInclude(r => r.Registrador)
            .Include(e => e.Estudiante)
            .Include(t => t.TipoDocumento)
            .OrderByDescending(f => f.FechaSolicitud)
            .ToListAsync();
    }
}
