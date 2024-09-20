using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using RegistroTecnicos.Models;
using System.Threading.Tasks;
using RegistroTecnicos.DAL;

namespace RegistroTecnicos.Services
{
    public class TrabajosServices
    {
        private readonly Contexto Contexto;

        public TrabajosServices(Contexto contexto)
        {
            Contexto = contexto;
        }

        //Método Existe
        public async Task<bool> Existe(int trabajoId)
        {
            return await Contexto.Trabajos.AnyAsync(t => t.TrabajoId == trabajoId);
        }

        //Método Insertar
        private async Task<bool> Insertar(Trabajos trabajo)
        {
            Contexto.Trabajos.Add(trabajo);
            return await Contexto.SaveChangesAsync() > 0;
        }

        //Método Modificar
        private async Task<bool> Modificar(Trabajos trabajo)
        {
            Contexto.Trabajos.Update(trabajo);
            var modificado = await Contexto.SaveChangesAsync() > 0;
            Contexto.Entry(trabajo).State = EntityState.Detached;
            return modificado;
        }

        //Método Guardar (decide si insertar o modificar)
        public async Task<bool> Guardar(Trabajos trabajo)
        {
            if (!await Existe(trabajo.TrabajoId))
                return await Insertar(trabajo);
            else
                return await Modificar(trabajo);
        }

        //Método Eliminar (compatibilidad con EF Core < 7.0)
        public async Task<bool> Eliminar(int id)
        {
            var trabajo = await Contexto.Trabajos.FindAsync(id);
            if (trabajo != null)
            {
                Contexto.Trabajos.Remove(trabajo);
                return await Contexto.SaveChangesAsync() > 0;
            }
            return false;
        }

        //Método Buscar (incluyendo Cliente y Técnico)
        public async Task<Trabajos?> Buscar(int id)
        {
            return await Contexto.Trabajos
                .Include(t => t.Cliente)
                .Include(t => t.Tecnico)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TrabajoId == id);
        }

        //Método Listar (criterio de búsqueda con relaciones)
        public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
        {
            return await Contexto.Trabajos
                .Include(t => t.Cliente)
                .Include(t => t.Tecnico)
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }
         private async Task EnviarMensajeWhatsApp(string numero, int trabajoId)
        {
            var mensaje = $"¡Hola! Su trabajo con ID {trabajoId} ya está listo. Gracias por confiar en nosotros.";
            var url = $"https://api.whatsapp.com/send?phone={numero}&text={Uri.EscapeDataString(mensaje)}";
            await Task.CompletedTask;
        }
    }
}
