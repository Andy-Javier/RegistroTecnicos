using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace RegistroTecnicos.Components.Services
{
    public class TecnicosServices
    {
        private readonly Contexto _context;
        public TecnicosServices(Contexto contexto)
        {
            _context = contexto;
        } 
        //Metodo Existe
        public async Task<bool> Existe(int TecnicoId)
        {
            return await _context.Tecnicos
                .AnyAsync(p => p.TecnicosId == TecnicoId);
        }
        //Metodo Insertar
        private async Task<bool> Insertar(Tecnicos Tecnicos)
        {
            _context.Tecnicos.Add(Tecnicos);
            return await _context.SaveChangesAsync() > 0;

        }
        // Método Modificar
        private async Task<bool> Modificar(Tecnicos Tecnicos)
        {
            _context.Update(Tecnicos);
            return await _context.SaveChangesAsync() > 0;
        }
        // Método guardar
        public async Task<bool> Guardar(Tecnicos Tecnico)
        {
            if (!await Existe(Tecnico.TecnicosId))
                return await Insertar(Tecnico);
            else
            {
                return await Modificar(Tecnico);
            }
        }
        public async Task<bool> Eliminar(int id)
        {
            var Tecnicos = await _context.Tecnicos.
                Where(T => T.TecnicosId == id).ExecuteDeleteAsync();
            
                return  Tecnicos > 0;        
        }
        public async Task<Tecnicos?> Buscar(int  id)
        {
            return await _context.Tecnicos.
                AsNoTracking()
                .FirstOrDefaultAsync(T => T.TecnicosId == id);
        }
        public List<Tecnicos> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            return _context.Tecnicos.
                AsNoTracking()
                .Where(criterio)
                .ToList();
        }
    }
}
