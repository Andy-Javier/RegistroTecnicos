﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services
{
    public class ClientesServices
    {
        private readonly Contexto Contexto;

        public ClientesServices(Contexto contexto)
        {
            Contexto = contexto;
        }

        public async Task<bool> Existe(int clienteId)
        {
            return await Contexto.Clientes.AnyAsync(c => c.ClienteId == clienteId);
        }

        private async Task<bool> Insertar(Clientes cliente)
        {
            Contexto.Clientes.Add(cliente);
            return await Contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Clientes cliente)
        {
            Contexto.Clientes.Update(cliente); 
            var modificado = await Contexto.SaveChangesAsync() > 0;
            Contexto.Entry(cliente).State = EntityState.Detached;
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
            var cliente = await Contexto.Clientes.FindAsync(id);
            if (cliente != null)
            {
                Contexto.Clientes.Remove(cliente);
                return await Contexto.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<Clientes?> Buscar(int id)
        {
            return await Contexto.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClienteId == id);
        }

        public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
        {
            return await Contexto.Clientes
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ExisteCliente(int clienteId, string nombres)
        {
            return await Contexto.Clientes
                .AnyAsync(c => c.ClienteId != clienteId && c.Nombres.ToLower() == nombres.ToLower());
        }
    }
}
