using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TrabajosServices
{
    private readonly Contexto _contexto;

    public TrabajosServices(Contexto contexto)
    {

        _contexto = contexto;

    }

    public async Task<bool> Insertar(Trabajos trabajos)
    {
        _contexto.Trabajos.Add(trabajos);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int trabajosId)
    {
        return await _contexto.Trabajos.AnyAsync(t => t.TrabajoId == trabajosId);

    }

    public async Task<bool> Modificar(Trabajos trabajos)
    {
        _contexto.Update(trabajos);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Trabajos trabajos)
    {

        if (!await Existe(trabajos.TrabajoId))
            return await Insertar(trabajos);
        else
        {
            return await Modificar(trabajos);
        }
    }

    public async Task<bool> Eliminar(int id)
    {
        var trabajos = await _contexto.Trabajos
            .Where(t => t.TrabajoId == id)
            .ExecuteDeleteAsync();
        return trabajos > 0;

    }

    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        return await _contexto.Trabajos
            .AsNoTracking()
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .Include(t => t.Prioridades)
            .Where(criterio)
            .OrderBy(t => t.Prioridades.PrioridadId)
            .ToListAsync();
    }

    public async Task<Trabajos?> Buscar(int id)
    {
        return await _contexto.Trabajos
            .AsNoTracking()
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .Include(t => t.Prioridades)
            .FirstOrDefaultAsync(t => t.TrabajoId == id);
    }



    public async Task<List<Clientes>> ObtenerClientes()
    {
        return await _contexto.Clientes.ToListAsync();
    }

    public async Task<List<Tecnicos>> ObtenerTecnicos()
    {
        return await _contexto.Tecnicos.ToListAsync();
    }
    public async Task<List<Prioridades>> ObtenerPrioridades()
    {
        return await _contexto.Prioridades.ToListAsync();
    }

    public async Task<List<TrabajosDetalle>> ObtenerDetalle()
    {
        return await _contexto.TrabajosDetalle.ToListAsync();
    }
    public async Task<List<Articulos>> ObtenerArticulos()
    {
        return await _contexto.Articulos.ToListAsync();
    }
}