using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TrabajosServices
    {
        private readonly ContextoFactory _contextoFactory;

        public TrabajosServices(ContextoFactory contextoFactory)
        {
            _contextoFactory = contextoFactory;
        }

        private Contexto GetContext()
        {
            return _contextoFactory.CreateDbContext(new string[] { });
        }

        public async Task<bool> Insertar(Trabajos trabajos)
        {
            using var contexto = GetContext();
            contexto.Trabajos.Add(trabajos);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Existe(int trabajosId)
        {
            using var contexto = GetContext();
            return await contexto.Trabajos.AnyAsync(t => t.TrabajoId == trabajosId);
        }

        public async Task<bool> Modificar(Trabajos trabajos)
        {
            using var contexto = GetContext();
            contexto.Update(trabajos);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Trabajos trabajos)
        {
            if (!await Existe(trabajos.TrabajoId))
                return await Insertar(trabajos);
            else
                return await Modificar(trabajos);
        }

        public async Task<bool> Eliminar(int id)
        {
            using var contexto = GetContext();
            var trabajos = await contexto.Trabajos
                .Where(t => t.TrabajoId == id)
                .ExecuteDeleteAsync();
            return trabajos > 0;
        }

        public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
        {
            using var contexto = GetContext();
            return await contexto.Trabajos
                .AsNoTracking()
                .Include(t => t.Cliente)
                .Include(t => t.Tecnico)
                .Include(t => t.Prioridades)
                .Where(criterio)
                .OrderBy(t => t.Prioridades.PrioridadId)
                .ToListAsync();
        }

        public async Task<Trabajos?> Buscar(int id)
        {
            using var contexto = GetContext();
            return await contexto.Trabajos
                .AsNoTracking()
                .Include(t => t.Cliente)
                .Include(t => t.Tecnico)
                .Include(t => t.Prioridades)
                .FirstOrDefaultAsync(t => t.TrabajoId == id);
        }

        public async Task<List<Clientes>> ObtenerClientes()
        {
            using var contexto = GetContext();
            return await contexto.Clientes.ToListAsync();
        }

        public async Task<List<Tecnicos>> ObtenerTecnicos()
        {
            using var contexto = GetContext();
            return await contexto.Tecnicos.ToListAsync();
        }

        public async Task<List<Prioridades>> ObtenerPrioridades()
        {
            using var contexto = GetContext();
            return await contexto.Prioridades.ToListAsync();
        }

        public async Task<List<TrabajosDetalle>> ObtenerDetalle()
        {
            using var contexto = GetContext();
            return await contexto.TrabajosDetalle.ToListAsync();
        }

        public async Task<List<Articulos>> ObtenerArticulos()
        {
            using var contexto = GetContext();
            return await contexto.Articulos.ToListAsync();
        }
    }
}
