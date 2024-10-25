using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class CotizacionesDetalleService
    {
        private readonly ContextoFactory _contextoFactory;

        public CotizacionesDetalleService(ContextoFactory contextoFactory)
        {
            _contextoFactory = contextoFactory;
        }

        private Contexto GetContext()
        {
            return _contextoFactory.CreateDbContext(new string[] { });
        }

        public async Task<bool> Insertar(CotizacionesDetalle detalle)
        {
            using var contexto = GetContext();
            contexto.CotizacionesDetalle.Add(detalle);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Existe(int detalleId)
        {
            using var contexto = GetContext();
            return await contexto.CotizacionesDetalle.AnyAsync(d => d.DetalleId == detalleId);
        }

        public async Task<bool> Modificar(CotizacionesDetalle detalle)
        {
            using var contexto = GetContext();
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
            using var contexto = GetContext();
            var detalle = await contexto.CotizacionesDetalle
                .Where(d => d.DetalleId == id)
                .ExecuteDeleteAsync();
            return detalle > 0;
        }

        public async Task<CotizacionesDetalle?> Buscar(int id)
        {
            using var contexto = GetContext();
            return await contexto.CotizacionesDetalle
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DetalleId == id);
        }

        public async Task<List<CotizacionesDetalle>> Listar(Expression<Func<CotizacionesDetalle, bool>> criterio)
        {
            using var contexto = GetContext();
            return await contexto.CotizacionesDetalle
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
