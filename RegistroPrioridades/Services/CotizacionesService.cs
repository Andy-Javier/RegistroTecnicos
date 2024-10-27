using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class CotizacionesService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;

    public CotizacionesService(IDbContextFactory<Contexto> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> Insertar(Cotizaciones cotizacion)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Cotizaciones.Add(cotizacion);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int cotizacionId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones.AnyAsync(c => c.CotizacionId == cotizacionId);
    }

    public async Task<bool> Modificar(Cotizaciones cotizacion)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Update(cotizacion);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Cotizaciones cotizacion)
    {
        if (!await Existe(cotizacion.CotizacionId))
            return await Insertar(cotizacion);
        else
            return await Modificar(cotizacion);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var cotizacionEliminada = await contexto.Cotizaciones
            .Where(c => c.CotizacionId == id)
            .ExecuteDeleteAsync();
        return cotizacionEliminada > 0;
    }

    public async Task<Cotizaciones?> Buscar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CotizacionId == id);
    }

    public async Task<List<Cotizaciones>> Listar(Expression<Func<Cotizaciones, bool>> criterio)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
