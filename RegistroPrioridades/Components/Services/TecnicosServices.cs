using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TecnicosServices
    {
        private readonly Contexto _context;

        public TecnicosServices(Contexto context)
        {
            _context = context;
        }

        // Método para verificar si existe un técnico por ID
        public async Task<bool> ExisteAsync(int tecnicoId)
        {
            return await _context.Tecnicos.AnyAsync(t => t.TecnicoId == tecnicoId);
        }

        // Método para insertar un nuevo técnico
        private async Task<bool> InsertarAsync(Tecnicos tecnico)
        {
            await _context.Tecnicos.AddAsync(tecnico);
            return await _context.SaveChangesAsync() > 0;
        }

        // Método para modificar un técnico existente
        private async Task<bool> ModificarAsync(Tecnicos tecnico)
        {
            _context.Tecnicos.Update(tecnico);
            return await _context.SaveChangesAsync() > 0;
        }

        // Método público para guardar (insertar o modificar) un técnico
        public async Task<bool> GuardarAsync(Tecnicos tecnico)
        {
            if (!await ExisteAsync(tecnico.TecnicoId))
                return await InsertarAsync(tecnico);
            else
                return await ModificarAsync(tecnico);
        }

        // Método para eliminar un técnico por ID
        public async Task<bool> EliminarAsync(int tecnicoId)
        {
            var tecnico = await _context.Tecnicos.FindAsync(tecnicoId);
            if (tecnico == null)
                return false;

            _context.Tecnicos.Remove(tecnico);
            return await _context.SaveChangesAsync() > 0;
        }

        // Método para buscar un técnico por ID
        public async Task<Tecnicos?> BuscarAsync(int tecnicoId)
        {
            return await _context.Tecnicos.AsNoTracking().FirstOrDefaultAsync(t => t.TecnicoId == tecnicoId);
        }

        // Método para listar técnicos basados en un criterio
        public async Task<List<Tecnicos>> ListarAsync(Expression<Func<Tecnicos, bool>> criterio)
        {
            return await _context.Tecnicos.AsNoTracking().Where(criterio).ToListAsync();
        }

        // Método para listar todos los técnicos
        public async Task<List<Tecnicos>> ListarTodosAsync()
        {
            return await _context.Tecnicos.AsNoTracking().ToListAsync();
        }
    }
}
