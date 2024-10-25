using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class CotizacionesService
    {
        private readonly ContextoFactory _contextoFactory;

        public CotizacionesService(ContextoFactory contextoFactory)
        {
            _contextoFactory = contextoFactory;
        }

        private Contexto GetContext()
        {
            return _contextoFactory.CreateDbContext(new string[] { });
        }

        public async Task<bool> Insertar(Cotizaciones cotizacion)
        {
            using var contexto = GetContext();
            contexto.Cotizaciones.Add(cotizacion);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Existe(int cotizacionId)
        {
            using var contexto = GetContext();
            return await contexto.Cotizaciones.AnyAsync(c => c.CotizacionId == cotizacionId);
        }

        public async Task<bool> Modificar(Cotizaciones cotizacion)
        {
            using var contexto = GetContext();
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
            using var contexto = GetContext();
            var cotizacion = await contexto.Cotizaciones
                .Where(c => c.CotizacionId == id)
                .ExecuteDeleteAsync();
            return cotizacion > 0;
        }

        public async Task<Cotizaciones?> Buscar(int id)
        {
            using var contexto = GetContext();
            return await contexto.Cotizaciones
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CotizacionId == id);
        }

        public async Task<List<Cotizaciones>> Listar(Expression<Func<Cotizaciones, bool>> criterio)
        {
            using var contexto = GetContext();
            return await contexto.Cotizaciones
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
