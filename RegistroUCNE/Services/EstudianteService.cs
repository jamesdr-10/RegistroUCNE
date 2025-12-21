using Microsoft.EntityFrameworkCore;
using RegistroUCNE.Data;
using RegistroUCNE.Models;
using System.Linq.Expressions;

namespace RegistroUCNE.Services;

public class EstudianteService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    private async Task<bool> Existe(int estudianteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes.AnyAsync(e => e.EstudianteId == estudianteId);
    }

    private async Task<bool> ExisteMatricula(string matricula)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes.AnyAsync(e => e.Matricula == matricula);
    }
    private async Task<bool> Insertar(Estudiante estudiante)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Estudiantes.Add(estudiante);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Estudiante estudiante)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Estudiantes.Update(estudiante);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Estudiante estudiante)
    {
        if (!await Existe(estudiante.EstudianteId))
        {
            if (await ExisteMatricula(estudiante.Matricula))
            {
                return false;
            }
            return await Insertar(estudiante);
        } else
        {
            var estudianteExistente = await Buscar(estudiante.EstudianteId);

            if (estudianteExistente!.Matricula != estudiante.Matricula && await ExisteMatricula(estudiante.Matricula)) 
            {
                return false;
            } else
            {
                return await Modificar(estudiante);
            }
        }
    }

    public async Task<bool> Deshabilitar(int estudianteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var estudiante = await contexto.Estudiantes
            .Include(s => s.SolicitudDocumentos)
            .FirstOrDefaultAsync(e => e.EstudianteId == estudianteId);

        if (estudiante == null)
        {
            return false;
        }

        estudiante.Activo = false;
        return await contexto.SaveChangesAsync() > 0;
    }

        public async Task<bool> Habilitar(int estudianteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var estudiante = await contexto.Estudiantes
            .Include(s => s.SolicitudDocumentos)
            .FirstOrDefaultAsync(e => e.EstudianteId == estudianteId);

        if (estudiante == null)
            return false;

        estudiante.Activo = true;

        foreach (var doc in estudiante.SolicitudDocumentos)
            doc.Activo = true;

        await contexto.SaveChangesAsync();
        return true;
    }

    public async Task<Estudiante?> Buscar(int estudianteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes
            .Include(s => s.SolicitudDocumentos)
            .FirstOrDefaultAsync(e => e.EstudianteId == estudianteId);
    }

    public async Task<List<Estudiante>> Listar(Expression<Func<Estudiante, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes
            .Include(s => s.SolicitudDocumentos)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
