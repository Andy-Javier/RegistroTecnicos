using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TrabajosService
    {
        private readonly IDbContextFactory<Contexto> _dbFactory;

        public TrabajosService(IDbContextFactory<Contexto> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<bool> Insertar(Trabajos trabajos)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Trabajos.Add(trabajos);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Existe(int trabajosId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Trabajos.AnyAsync(t => t.TrabajoId == trabajosId);
        }

        public async Task<bool> Modificar(Trabajos trabajos)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
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
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var trabajos = await contexto.Trabajos
                .Where(t => t.TrabajoId == id)
                .ExecuteDeleteAsync();
            return trabajos > 0;
        }

        public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
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
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Trabajos
                .AsNoTracking()
                .Include(t => t.Cliente)
                .Include(t => t.Tecnico)
                .Include(t => t.Prioridades)
                .FirstOrDefaultAsync(t => t.TrabajoId == id);
        }

        public async Task<Trabajos?> GetById(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Trabajos
                .AsNoTracking()
                .Include(t => t.Cliente)
                .Include(t => t.Tecnico)
                .Include(t => t.Prioridades)
                .FirstOrDefaultAsync(t => t.TrabajoId == id);
        }

        public async Task<List<TrabajosDetalle>> ListarDetalles(int trabajosId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.TrabajosDetalle
                .AsNoTracking()
                .Where(td => td.TrabajosId == trabajosId)
                .ToListAsync();
        }


        public async Task<List<Clientes>> ObtenerClientes()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Clientes.ToListAsync();
        }

        public async Task<List<Tecnicos>> ObtenerTecnicos()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos.ToListAsync();
        }

        public async Task<List<Prioridades>> ObtenerPrioridades()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Prioridades.ToListAsync();
        }

        public async Task<List<TrabajosDetalle>> ObtenerDetalle()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.TrabajosDetalle.ToListAsync();
        }

        public async Task<List<Articulos>> ObtenerArticulos()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Articulos.ToListAsync();
        }
    }
}
