using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;
using RegistroTecnicos.DAL;

namespace RegistroTecnicos.Services;

public class TrabajosDetalleServices
{
    private readonly Contexto _contexto;

    public TrabajosDetalleServices(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Guardar(TrabajosDetalle detalle)
    {
        if (detalle.DetalleId == 0)
            _contexto.Set<TrabajosDetalle>().Add(detalle);
        else
            _contexto.Set<TrabajosDetalle>().Update(detalle);

        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<TrabajosDetalle> Buscar(int id)
    {
        return await _contexto.Set<TrabajosDetalle>().FindAsync(id);
    }

    public async Task<bool> Eliminar(int id)
    {
        var detalle = await _contexto.Set<TrabajosDetalle>().FindAsync(id);
        if (detalle != null)
        {
            _contexto.Set<TrabajosDetalle>().Remove(detalle);
            return await _contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<TrabajosDetalle>> Listar()
    {
        return await _contexto.Set<TrabajosDetalle>().ToListAsync();
    }
}
