using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using System.Linq.Expressions;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services
{
    public class TiposTecnicosService
    {
        private readonly IDbContextFactory<Contexto> _dbFactory;

        public TiposTecnicosService(IDbContextFactory<Contexto> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<bool> ExisteTipoId(int tiposTecnicosId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.TiposTecnicos.AnyAsync(t => t.TipoTecnicoId == tiposTecnicosId);
        }

        public async Task<bool> ExisteTipoDescripcion(string? descripcion)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.TiposTecnicos.AnyAsync(t => t.Descripcion == descripcion);
        }

        private async Task<bool> Insertar(TiposTecnicos tiposTecnicos)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            await contexto.TiposTecnicos.AddAsync(tiposTecnicos);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(TiposTecnicos tiposTecnicos)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.TiposTecnicos.Update(tiposTecnicos);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(TiposTecnicos tiposTecnicos)
        {
            if (!await ExisteTipoId(tiposTecnicos.TipoTecnicoId))
                return await Insertar(tiposTecnicos);
            else
                return await Modificar(tiposTecnicos);
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.TiposTecnicos
                .Where(tipos => tipos.TipoTecnicoId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<TiposTecnicos?> Buscar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.TiposTecnicos
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TipoTecnicoId == id);
        }

        public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.TiposTecnicos
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<bool> ExisteTipoTecnico(int id, string? descripcion)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.TiposTecnicos.AnyAsync(e => e.TipoTecnicoId != id && e.Descripcion.ToLower().Equals(descripcion.ToLower()));
        }
    }
}
