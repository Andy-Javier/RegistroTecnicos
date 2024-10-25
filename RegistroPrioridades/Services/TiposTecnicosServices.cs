using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using System.Linq.Expressions;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services
{
    public class TiposTecnicosServices
    {
        private readonly ContextoFactory _contextoFactory;

        public TiposTecnicosServices(ContextoFactory contextoFactory)
        {
            _contextoFactory = contextoFactory;
        }

        private Contexto GetContext()
        {
            return _contextoFactory.CreateDbContext(new string[] { });
        }

        public async Task<bool> ExisteTipoId(int tiposTecnicosId)
        {
            using var contexto = GetContext();
            return await contexto.TiposTecnicos.AnyAsync(t => t.TipoTecnicoId == tiposTecnicosId);
        }

        public async Task<bool> ExisteTipoDescripcion(string? descripcion)
        {
            using var contexto = GetContext();
            return await contexto.TiposTecnicos.AnyAsync(t => t.Descripcion == descripcion);
        }

        private async Task<bool> Insertar(TiposTecnicos tiposTecnicos)
        {
            using var contexto = GetContext();
            await contexto.TiposTecnicos.AddAsync(tiposTecnicos);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(TiposTecnicos tiposTecnicos)
        {
            using var contexto = GetContext();
            contexto.TiposTecnicos.Update(tiposTecnicos);
            var modificado = await contexto.SaveChangesAsync() > 0;
            return modificado;
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
            using var contexto = GetContext();
            return await contexto.TiposTecnicos
                .Where(tipos => tipos.TipoTecnicoId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<TiposTecnicos?> Buscar(int id)
        {
            using var contexto = GetContext();
            return await contexto.TiposTecnicos
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TipoTecnicoId == id);
        }

        public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
        {
            using var contexto = GetContext();
            return await contexto.TiposTecnicos
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<bool> ExisteTipoTecnico(int id, string? descripcion)
        {
            using var contexto = GetContext();
            return await contexto.TiposTecnicos.AnyAsync(e => e.TipoTecnicoId != id && e.Descripcion.ToLower().Equals(descripcion.ToLower()));
        }
    }
}
