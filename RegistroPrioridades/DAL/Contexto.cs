using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Trabajos> Trabajos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Tecnicos> Tecnicos { get; set; }
        public DbSet<Prioridades> Prioridades { get; set; }
        public DbSet<TiposTecnicos> TiposTecnicos { get; set; }
        public DbSet<Articulos> Articulos { get; set; } // Nueva tabla Articulos
        public DbSet<TrabajosDetalle> TrabajosDetalle { get; set; } // Nueva tabla TrabajosDetalle

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para Trabajos
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

            // Configuración para TiposTecnicos
            modelBuilder.Entity<TiposTecnicos>()
                .HasMany(t => t.Tecnicos)
                .WithOne(t => t.TipoTecnico)
                .HasForeignKey(t => t.TipoTecnicoId);

            // Configuración para Articulos
            modelBuilder.Entity<Articulos>(entity =>
            {
                entity.HasKey(e => e.ArticuloId);
                entity.Property(e => e.Descripcion).IsRequired();
                entity.Property(e => e.Costo).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Precio).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Existencia).IsRequired();
            });

            // Configuración para TrabajosDetalle
            modelBuilder.Entity<TrabajosDetalle>(entity =>
            {
                entity.HasKey(e => e.DetalleId);
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.Precio).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Costo).IsRequired().HasColumnType("decimal(18,2)");

                // Relación entre TrabajosDetalle y Trabajos
                entity.HasOne(td => td.Trabajo)
                      .WithMany(t => t.TrabajosDetalles)
                      .HasForeignKey(td => td.TrabajoId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relación entre TrabajosDetalle y Articulos
                entity.HasOne(td => td.Articulo)
                      .WithMany()
                      .HasForeignKey(td => td.ArticuloId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
