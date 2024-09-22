using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

public class PrioridadesServices
{
    private readonly Contexto _contexto;

    public PrioridadesServices(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Prioridades>> Listar(Func<Prioridades, bool> criterio)
    {
        return await Task.FromResult(_contexto.Prioridades.Where(criterio).ToList());
    }

    public async Task<Prioridades?> Buscar(int PrioridadId)
    {
        return await _contexto.Prioridades.FindAsync(PrioridadId);
    }

    public async Task<bool> Guardar(Prioridades prioridad)
    {
        if (await _contexto.Prioridades.AnyAsync(p => p.PrioridadId == prioridad.PrioridadId))
            _contexto.Prioridades.Update(prioridad);
        else
            await _contexto.Prioridades.AddAsync(prioridad);

        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(Prioridades prioridad)
    {
        _contexto.Prioridades.Remove(prioridad);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Prioridades prioridad)
    {
        var existente = await _contexto.Prioridades.FindAsync(prioridad.PrioridadId);
        if (existente == null)
            return false;

        existente.Descripcion = prioridad.Descripcion;
        existente.Tiempo = prioridad.Tiempo;

        _contexto.Prioridades.Update(existente);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Prioridades>> GetPrioridades()
    {
        return await _contexto.Prioridades.ToListAsync();
    }
}
