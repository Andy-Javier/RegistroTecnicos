using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;
using RegistroTecnicos.DAL;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TecnicosService
    {
        private readonly ContextoFactory _contextoFactory;

        public TecnicosService(ContextoFactory contextoFactory)
        {
            _contextoFactory = contextoFactory;
        }

        private Contexto GetContext()
        {
            return _contextoFactory.CreateDbContext(new string[] { });
        }

        public async Task<bool> Existe(int TecnicoId)
        {
            using var contexto = GetContext();
            return await contexto.Tecnicos.AnyAsync(p => p.TecnicoId == TecnicoId);
        }

        private async Task<bool> Insertar(Tecnicos tecnico)
        {
            using var contexto = GetContext();
            contexto.Tecnicos.Add(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Tecnicos tecnico)
        {
            using var contexto = GetContext();
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
            using var contexto = GetContext();
            var Tecnicos = await contexto.Tecnicos.Where(p => p.TecnicoId == id).ExecuteDeleteAsync();
            return Tecnicos > 0;
        }

        public async Task<Tecnicos?> Buscar(int id)
        {
            using var contexto = GetContext();
            return await contexto.Tecnicos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.TecnicoId == id);
        }

        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            using var contexto = GetContext();
            return await contexto.Tecnicos
                .Include(t => t.TipoTecnico)
                .Where(criterio)
                .ToListAsync();
        }
    }
}
