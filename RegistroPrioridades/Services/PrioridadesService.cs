using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

public class PrioridadesService
{
    private readonly ContextoFactory _contextoFactory;

    public PrioridadesService(ContextoFactory contextoFactory)
    {
        _contextoFactory = contextoFactory;
    }

    private Contexto GetContext()
    {
        return _contextoFactory.CreateDbContext(new string[] { });
    }

    public async Task<List<Prioridades>> Listar(Func<Prioridades, bool> criterio)
    {
        using var contexto = GetContext();
        return await Task.FromResult(contexto.Prioridades.Where(criterio).ToList());
    }

    public async Task<Prioridades?> Buscar(int PrioridadId)
    {
        using var contexto = GetContext();
        return await contexto.Prioridades.FindAsync(PrioridadId);
    }

    public async Task<bool> Guardar(Prioridades prioridad)
    {
        using var contexto = GetContext();
        if (await contexto.Prioridades.AnyAsync(p => p.PrioridadId == prioridad.PrioridadId))
            contexto.Prioridades.Update(prioridad);
        else
            await contexto.Prioridades.AddAsync(prioridad);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(Prioridades prioridad)
    {
        using var contexto = GetContext();
        contexto.Prioridades.Remove(prioridad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Prioridades prioridad)
    {
        using var contexto = GetContext();
        var existente = await contexto.Prioridades.FindAsync(prioridad.PrioridadId);
        if (existente == null)
            return false;
        existente.Descripcion = prioridad.Descripcion;
        existente.Tiempo = prioridad.Tiempo;
        contexto.Prioridades.Update(existente);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Prioridades>> GetPrioridades()
    {
        using var contexto = GetContext();
        return await contexto.Prioridades.ToListAsync();
    }
}
