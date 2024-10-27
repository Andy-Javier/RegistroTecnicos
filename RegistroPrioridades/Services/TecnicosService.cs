using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;
using RegistroTecnicos.DAL;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TecnicosService
    {
        private readonly IDbContextFactory<Contexto> _dbFactory;

        public TecnicosService(IDbContextFactory<Contexto> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<bool> Existe(int tecnicoId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos.AnyAsync(p => p.TecnicoId == tecnicoId);
        }

        private async Task<bool> Insertar(Tecnicos tecnico)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Tecnicos.Add(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Tecnicos tecnico)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Tecnicos.Update(tecnico);
            var modificado = await contexto.SaveChangesAsync() > 0;
            contexto.Entry(tecnico).State = EntityState.Detached;
            return modificado;
        }

        public async Task<bool> Guardar(Tecnicos tecnico)
        {
            if (!await Existe(tecnico.TecnicoId))
                return await Insertar(tecnico);
            else
                return await Modificar(tecnico);
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var eliminado = await contexto.Tecnicos.Where(p => p.TecnicoId == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<Tecnicos?> Buscar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.TecnicoId == id);
        }

        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .Include(t => t.TipoTecnico)
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
