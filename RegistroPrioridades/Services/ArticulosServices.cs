using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class ArticulosServices
{
    private readonly Contexto _contexto;

    public ArticulosServices(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Articulos>> ListaArticulos()
    {
        return await _contexto.Articulos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Articulos?> ObtenerArticuloPorId(int id)
    {
        return await _contexto.Articulos
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ArticuloId == id);
    }

    public async Task ActualizarExistencia(int articuloId, decimal cantidad)
    {
        var articulo = await _contexto.Articulos.FindAsync(articuloId);
        if (articulo != null)
        {
            articulo.Existencia -= (int)cantidad;
            _contexto.Articulos.Update(articulo);
            await _contexto.SaveChangesAsync();
        }
    }


    public async Task AgregarCantidad(int articuloId, int cantidad)
    {
        var articulo = await _contexto.Articulos.FindAsync(articuloId);

        if (articulo != null)
        {
            articulo.Existencia += cantidad;

            await _contexto.SaveChangesAsync();
        }
    }

}