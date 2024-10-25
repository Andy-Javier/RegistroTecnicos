using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class ArticuloService
    {
        private readonly ContextoFactory _contextoFactory;

        public ArticuloService(ContextoFactory contextoFactory)
        {
            _contextoFactory = contextoFactory;
        }

        private Contexto GetContext()
        {
            return _contextoFactory.CreateDbContext(new string[] { });
        }

        public async Task<List<Articulos>> ListaArticulos()
        {
            using var _contexto = GetContext();
            return await _contexto.Articulos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Articulos?> ObtenerArticuloPorId(int id)
        {
            using var _contexto = GetContext();
            return await _contexto.Articulos
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ArticuloId == id);
        }

        public async Task ActualizarExistencia(int articuloId, decimal cantidad)
        {
            using var _contexto = GetContext();
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
            using var _contexto = GetContext();
            var articulo = await _contexto.Articulos.FindAsync(articuloId);
            if (articulo != null)
            {
                articulo.Existencia += cantidad;
                await _contexto.SaveChangesAsync();
            }
        }
    }
}
