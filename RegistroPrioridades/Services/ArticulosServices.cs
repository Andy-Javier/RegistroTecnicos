using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services;

public class ArticulosServices
{
    private readonly Contexto _contexto;

    public ArticulosServices(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Guardar(Articulos articulo)
    {
        if (articulo.ArticuloId == 0)
            _contexto.Set<Articulos>().Add(articulo); 
        else
            _contexto.Set<Articulos>().Update(articulo);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<Articulos> Buscar(int id)
    {
        return await _contexto.Set<Articulos>().FindAsync(id);
    }

    public async Task<bool> Eliminar(int id)
    {
        var articulo = await _contexto.Set<Articulos>().FindAsync(id); 
        if (articulo != null)
        {
            _contexto.Set<Articulos>().Remove(articulo); 
            return await _contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<Articulos>> Listar()
    {
        return await _contexto.Set<Articulos>().ToListAsync(); 
    }
}
