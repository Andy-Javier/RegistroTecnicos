using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;
using RegistroTecnicos.Services;

namespace RegistroTecnicos.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options) { }
        public DbSet<Trabajos> Trabajos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Tecnicos> Tecnicos { get; set; }
        public DbSet<Prioridades> Prioridades { get; set; }
        public DbSet<TiposTecnicos> TiposTecnicos { get; set; }
        public DbSet<Articulos> Articulos { get; set; }
        public DbSet<TrabajosDetalle> TrabajosDetalle { get; set; }
        public DbSet<Cotizaciones> Cotizaciones { get; set; }
        public DbSet<CotizacionesDetalle> CotizacionesDetalle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
