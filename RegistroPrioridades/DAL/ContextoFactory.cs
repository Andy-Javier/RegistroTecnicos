using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RegistroTecnicos.DAL;
using System.IO;

public class ContextoFactory : IDesignTimeDbContextFactory<Contexto>
{
    public Contexto CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<Contexto>();
        optionsBuilder.UseSqlite("Data Source=app.db");

        return new Contexto(optionsBuilder.Options);
    }
}
