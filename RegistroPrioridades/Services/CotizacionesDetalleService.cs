using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class CotizacionesDetalleService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;

    public CotizacionesDetalleService(IDbContextFactory<Contexto> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> Insertar(CotizacionesDetalle detalle)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.CotizacionesDetalle.Add(detalle);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int detalleId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.CotizacionesDetalle.AnyAsync(d => d.DetalleId == detalleId);
    }

    public async Task<bool> Modificar(CotizacionesDetalle detalle)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Update(detalle);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(CotizacionesDetalle detalle)
    {
        if (!await Existe(detalle.DetalleId))
            return await Insertar(detalle);
        else
            return await Modificar(detalle);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var detallesEliminados = await contexto.CotizacionesDetalle
            .Where(d => d.DetalleId == id)
            .ExecuteDeleteAsync();
        return detallesEliminados > 0;
    }

    public async Task<CotizacionesDetalle?> Buscar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.CotizacionesDetalle
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DetalleId == id);
    }

    public async Task<List<CotizacionesDetalle>> Listar(Expression<Func<CotizacionesDetalle, bool>> criterio)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.CotizacionesDetalle
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
