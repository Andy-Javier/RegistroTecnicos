using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class ClientesService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;

    public ClientesService(IDbContextFactory<Contexto> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> Existe(int clienteId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AnyAsync(c => c.ClienteId == clienteId);
    }

    private async Task<bool> Insertar(Clientes cliente)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Clientes.Add(cliente);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Clientes cliente)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Clientes.Update(cliente);
        var modificado = await contexto.SaveChangesAsync() > 0;
        contexto.Entry(cliente).State = EntityState.Detached;
        return modificado;
    }

    public async Task<bool> Guardar(Clientes cliente)
    {
        if (!await Existe(cliente.ClienteId))
            return await Insertar(cliente);
        else
            return await Modificar(cliente);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var cliente = await contexto.Clientes.FindAsync(id);
        if (cliente != null)
        {
            contexto.Clientes.Remove(cliente);
            return await contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<Clientes?> Buscar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ClienteId == id);
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> ExisteCliente(int clienteId, string nombres)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AnyAsync(c => c.ClienteId != clienteId && c.Nombres.ToLower() == nombres.ToLower());
    }

    // Método nuevo para buscar por nombre
    public async Task<Clientes?> BuscarPorNombre(string nombres)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Nombres.ToLower().Contains(nombres.ToLower()));
    }
}
