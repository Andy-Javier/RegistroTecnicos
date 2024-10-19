using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
namespace RegistroTecnicos.Services;

public class ArticulosServices
{
    private readonly DbContext _context;

    public ArticulosServices(DbContext context)
    {
        _context = context;
    }

    public async Task<bool> Guardar(Articulos articulo)
    {
        if (articulo.ArticuloId == 0)
            _context.Set<Articulos>().Add(articulo);
        else
            _context.Set<Articulos>().Update(articulo);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Articulos> Buscar(int id)
    {
        return await _context.Set<Articulos>().FindAsync(id);
    }

    public async Task<bool> Eliminar(int id)
    {
        var articulo = await _context.Set<Articulos>().FindAsync(id);
        if (articulo != null)
        {
            _context.Set<Articulos>().Remove(articulo);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<Articulos>> Listar()
    {
        return await _context.Set<Articulos>().ToListAsync();
    }
}
