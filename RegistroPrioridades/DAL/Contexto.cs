using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) // Constructor que acepta DbContextOptions
        {
        }

        public DbSet<Trabajos> Trabajos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Tecnicos> Tecnicos { get; set; }
        public DbSet<Prioridades> Prioridades { get; set; }
        public DbSet<TiposTecnicos> TiposTecnicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trabajos>()
                .HasOne(t => t.Cliente)
                .WithMany()
                .HasForeignKey(t => t.ClienteId);

            modelBuilder.Entity<Trabajos>()
                .HasOne(t => t.Tecnico)
                .WithMany()
                .HasForeignKey(t => t.TecnicoId);

            modelBuilder.Entity<Trabajos>()
                .HasOne(t => t.Prioridad)
                .WithMany()
                .HasForeignKey(t => t.PrioridadId);

            modelBuilder.Entity<TiposTecnicos>()
                .HasMany(t => t.Tecnicos)
                .WithOne(t => t.TipoTecnico)
                .HasForeignKey(t => t.TipoTecnicoId);

        }
    }
}
