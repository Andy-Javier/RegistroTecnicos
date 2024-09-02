using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TecnicosServices
    {
        private readonly Contexto Contexto;

        public TecnicosServices(Contexto contexto)
        {
            Contexto = contexto;
        }

        // Método Existe
        public async Task<bool> Existe(int TecnicoId)
        {
            return await Contexto.Tecnicos.AnyAsync(t => t.TecnicoId == TecnicoId);
        }

        // Método Insertar
        private async Task<bool> Insertar(Tecnicos tecnico)
        {
            await Contexto.Tecnicos.AddAsync(tecnico);
            return await Contexto.SaveChangesAsync() > 0;
        }

        // Método Modificar
        private async Task<bool> Modificar(Tecnicos tecnico)
        {
            Contexto.Tecnicos.Update(tecnico);
            var modificado = await Contexto.SaveChangesAsync() > 0;
            Contexto.Entry(tecnico).State = EntityState.Detached;
            return modificado;
        }

        // Método Guardar
        public async Task<bool> Guardar(Tecnicos tecnico)
        {
            bool existeNombre = await Contexto.Tecnicos.AnyAsync(t => t.Nombre == tecnico.Nombre && t.TecnicoId != tecnico.TecnicoId);
            if (existeNombre)
            {
                throw new Exception("Ya existe un técnico con este nombre.");
            }

            if (!await Existe(tecnico.TecnicoId))
                return await Insertar(tecnico);
            else
                return await Modificar(tecnico);
        }


        // Método Eliminar
        public async Task<bool> Eliminar(int id)
        {
            var tecnico = await Buscar(id);
            if (tecnico != null)
            {
                Contexto.Tecnicos.Remove(tecnico);
                return await Contexto.SaveChangesAsync() > 0;
            }
            return false;
        }

        // Método Buscar
        public async Task<Tecnicos?> Buscar(int id)
        {
            return await Contexto.Tecnicos
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TecnicoId == id);
        }

        // Método Listar
        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            IQueryable<Tecnicos> tecnicos = Contexto.Tecnicos
                            .AsNoTracking()
                            .Where(criterio);
            return await tecnicos
                .ToListAsync();
        }
    }
}
