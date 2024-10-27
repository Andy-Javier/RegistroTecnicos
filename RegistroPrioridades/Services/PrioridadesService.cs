using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;
public class PrioridadesService(IDbContextFactory<Contexto> DbFactory)
{

    //Metodo del existe
    public async Task<bool> Existe(int prioridadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades.AnyAsync(p => p.PrioridadId == prioridadId);
    }
    // Método del Insertar
    private async Task<bool> Insertar(Prioridades prioridades)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Prioridades.Add(prioridades);
        return await contexto.SaveChangesAsync() > 0;
    }

    // Método del Modificar
    public async Task<bool> Modificar(Prioridades prioridad) // Cambiado a public
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Prioridades.Update(prioridad);
        return await contexto.SaveChangesAsync() > 0;
    }
    //Metodo el Guardar
    public async Task<bool> Guardar(Prioridades prioridad)
    {
        if (!await Existe(prioridad.PrioridadId))
        {
            return await Insertar(prioridad);
        }
        else
        {
            return await Modificar(prioridad);
        }
    }
    //Metodo Eliminar
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var eliminado = await contexto.Prioridades
            .Where(p => p.PrioridadId == id)
            .ExecuteDeleteAsync();
        return eliminado > 0;
    }
    //Metodo del Buscar
    public async Task<Prioridades?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PrioridadId == id);
    }
    //Metodo del listar
    public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> Criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
            .AsNoTracking()
            .Where(Criterio)
            .ToListAsync();
    }
}