using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;
using RegistroTecnicos.DAL;
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

        public async Task<bool> Existe(int TecnicoId)
        {
            return await Contexto.Tecnicos.AnyAsync(p => p.TecnicoId == TecnicoId);

        }

        private async Task<bool> Insertar(Tecnicos tecnico)
        {
            Contexto.Tecnicos.Add(tecnico);
            return await Contexto.SaveChangesAsync() > 0;
        }


        private async Task<bool> Modificar(Tecnicos tecnico)
        {
            Contexto.Tecnicos.Update(tecnico);
            var modificado = await Contexto.SaveChangesAsync() > 0;
            Contexto.Entry(tecnico).State = EntityState.Detached;
            return modificado;
        }

        public async Task<bool> Guardar(Tecnicos tecnico)
        {
            if (!await Existe(tecnico.TecnicoId))
                return await Insertar(tecnico);
            else
            {
                return await Modificar(tecnico);
            }
        }


        public async Task<bool> Eliminar(int id)
        {
            var Tecnicos = await Contexto.Tecnicos.Where(P => P.TecnicoId == id).ExecuteDeleteAsync();
            return Tecnicos > 0;
        }


        public async Task<Tecnicos?> Buscar(int id)
        {
            return await Contexto.Tecnicos
                .AsNoTracking()
                .FirstOrDefaultAsync(P => P.TecnicoId == id);
        }


        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            return await Contexto.Tecnicos
                .Include(t => t.TipoTecnico)
                .Where(criterio)
                .ToListAsync();
        }
    }
}
