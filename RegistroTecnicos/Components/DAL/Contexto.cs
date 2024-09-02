using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

public class Contexto : DbContext
{
    public DbSet<Tecnicos> Tecnicos { get; set; }

    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=path_to_your_database.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tecnicos>(entity =>
        {
            entity.HasIndex(e => e.Nombre).IsUnique(); // Para evitar nombres duplicados
        });
    }
}
