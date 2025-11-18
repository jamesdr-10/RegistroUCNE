using Microsoft.EntityFrameworkCore;
using RegistroUCNE.DAL;
using RegistroUCNE.Models;
using System.Linq.Expressions;

namespace RegistroUCNE.Services;

public class RegistradorService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int registradorId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Registradores.AnyAsync(r => r.RegistradorId == registradorId);
    }

    private async Task<bool> ExisteUsuario(string nombreUsuario)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Registradores.AnyAsync(r => r.NombreUsuario == nombreUsuario);
    }

    public async Task<bool> ExisteCredenciales(string nombreUsuario, string contrasena)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var registrador = await contexto.Registradores.FirstOrDefaultAsync(r => r.NombreUsuario == nombreUsuario);

        if (registrador == null)
        {
            return false;
        }

        return BCrypt.Net.BCrypt.Verify(contrasena, registrador.Contrasena);
    }

    private async Task<bool> Insertar(Registrador registrador)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Registradores.Add(registrador);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Registrador registrador)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Registradores.Update(registrador);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Registrador registrador)
    {
        if (!string.IsNullOrWhiteSpace(registrador.Contrasena))
        {
            bool esHashBCryptValido = false;

            try
            {
                if (registrador.Contrasena.Length == 60 &&
                    (registrador.Contrasena.StartsWith("$2a$") ||
                    registrador.Contrasena.StartsWith("$2b$") ||
                    registrador.Contrasena.StartsWith("$2y$")))
                {
                    esHashBCryptValido = true;
                }
            }
            catch
            {
                esHashBCryptValido = false;
            }

            if (!esHashBCryptValido)
            {
                registrador.Contrasena = BCrypt.Net.BCrypt.HashPassword(registrador.Contrasena);
            }
        }

        if (!await Existe(registrador.RegistradorId))
        {
            if (await ExisteUsuario(registrador.NombreUsuario))
            {
                return false;
            }
            return await Insertar(registrador);
        } else
        {
            var registradorExistente = await BuscarPorId(registrador.RegistradorId);

            if (registradorExistente!.NombreUsuario != registrador.NombreUsuario && await ExisteUsuario(registrador.NombreUsuario))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(registrador.Contrasena))
            {
                registrador.Contrasena = registradorExistente.Contrasena;
            }

            return await Modificar(registrador);
        }
    }

    public async Task<bool> Deshabilitar(int registradorId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var registrador = await contexto.Registradores.FindAsync(registradorId);

        if (registrador == null)
        {
            return false;
        }

        registrador.Activo = false;
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Habilitar(int estudianteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var registrador = await contexto.Registradores.FindAsync(estudianteId);

        if (registrador == null)
            return false;

        registrador.Activo = true;
        await contexto.SaveChangesAsync();
        return true;
    }

    public async Task<Registrador?> BuscarPorCredenciales(string nombreUsuario, string contrasena)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var registrador = await contexto.Registradores
        .Include(d => d.DocumentoRegistradores)
        .FirstOrDefaultAsync(r => r.NombreUsuario == nombreUsuario);

        if (registrador == null)
        {
            return null;
        }

        return BCrypt.Net.BCrypt.Verify(contrasena, registrador.Contrasena) ? registrador : null;
    }

    public async Task<Registrador?> BuscarPorId(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Registradores
            .Include(d => d.DocumentoRegistradores)
            .FirstOrDefaultAsync(r => r.RegistradorId == id);
    }

    public async Task<List<Registrador>> Listar(Expression<Func<Registrador, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Registradores
            .Include(e => e.DocumentoRegistradores)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
