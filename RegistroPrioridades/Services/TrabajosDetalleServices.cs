using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;
namespace RegistroTecnicos.Services;

public class TrabajosDetalleServices
{
    private readonly DbContext _context;

    public TrabajosDetalleServices(DbContext context)
    {
        _context = context;
    }

    public async Task<bool> Guardar(TrabajosDetalle detalle)
    {
        if (detalle.DetalleId == 0)
            _context.Set<TrabajosDetalle>().Add(detalle);
        else
            _context.Set<TrabajosDetalle>().Update(detalle);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<TrabajosDetalle> Buscar(int id)
    {
        return await _context.Set<TrabajosDetalle>().FindAsync(id);
    }

    public async Task<bool> Eliminar(int id)
    {
        var detalle = await _context.Set<TrabajosDetalle>().FindAsync(id);
        if (detalle != null)
        {
            _context.Set<TrabajosDetalle>().Remove(detalle);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<TrabajosDetalle>> Listar()
    {
        return await _context.Set<TrabajosDetalle>().ToListAsync();
    }
}
