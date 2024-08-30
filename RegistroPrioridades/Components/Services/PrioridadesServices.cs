using Microsoft.EntityFrameworkCore;
using RegistroPrioridades.DAL;
using RegistroPrioridades.Models;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace RegistroPrioridades.Components.Services
{
    public class PrioridadesServices
    {
        private readonly Contexto _context;
        public PrioridadesServices(Contexto contexto)
        {
            _context = contexto;
        } 
        //Metodo Existe
        public async Task<bool> Existe(int PrioridadId)
        {
            return await _context.Prioridades
                .AnyAsync(p => p.PrioridadId == PrioridadId);
        }
        //Metodo Insertar
        private async Task<bool> Insertar(Prioridades Prioridades)
        {
            _context.Prioridades.Add(Prioridades);
            return await _context.SaveChangesAsync() > 0;

        }
        // Método Modificar
        private async Task<bool> Modificar(Prioridades Prioridades)
        {
            _context.Update(Prioridades);
            return await _context.SaveChangesAsync() > 0;
        }
        // Método guardar
        public async Task<bool> Guardar(Prioridades Prioridad)
        {
            if (!await Existe(Prioridad.PrioridadId))
                return await Insertar(Prioridad);
            else
            {
                return await Modificar(Prioridad);
            }
        }
    }
}
