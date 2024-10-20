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
        public DbSet<Articulos> Articulos { get; set; }
        public DbSet<TrabajosDetalle> TrabajosDetalle { get; set; }

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
                .HasOne(t => t.Prioridades)
                .WithMany()
                .HasForeignKey(t => t.PrioridadId);

            modelBuilder.Entity<TiposTecnicos>()
                .HasMany(t => t.Tecnicos)
                .WithOne(t => t.TipoTecnico)
                .HasForeignKey(t => t.TipoTecnicoId);

            modelBuilder.Entity<Articulos>(entity =>
            {
                entity.HasKey(e => e.ArticuloId);
                entity.Property(e => e.Descripcion).IsRequired();
                entity.Property(e => e.Costo).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Precio).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Existencia).IsRequired();
            });

            modelBuilder.Entity<TrabajosDetalle>(entity =>
            {
                entity.HasKey(e => e.DetalleId);
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.Precio).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Costo).IsRequired().HasColumnType("decimal(18,2)");

                entity.HasOne(td => td.Trabajos)
                      .WithMany(t => t.TrabajosDetalle)
                      .HasForeignKey(td => td.TrabajosId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(td => td.Articulos)
                      .WithMany()
                      .HasForeignKey(td => td.ArticuloId)
                      .OnDelete(DeleteBehavior.Restrict);
            });           
            modelBuilder.Entity<Articulos>().HasData(
                new Articulos
                {
                    ArticuloId = 1,
                    Descripcion = "RTX 4060",
                    Costo = 300.0m,
                    Precio = 400.0m,
                    Existencia = 15
                },
                new Articulos
                {
                    ArticuloId = 2,
                    Descripcion = "RTX 4070",
                    Costo = 450.0m,
                    Precio = 600.0m,
                    Existencia = 10
                },
                new Articulos
                {
                    ArticuloId = 3,
                    Descripcion = "RTX 4070 Ti",
                    Costo = 650.0m,
                    Precio = 800.0m,
                    Existencia = 8
                },
                new Articulos
                {
                    ArticuloId = 4,
                    Descripcion = "RTX 4080",
                    Costo = 900.0m,
                    Precio = 1200.0m,
                    Existencia = 5
                },
                new Articulos
                {
                    ArticuloId = 5,
                    Descripcion = "RTX 4090",
                    Costo = 1600.0m,
                    Precio = 2100.0m,
                    Existencia = 3
                }
            );
        }
    }
}
