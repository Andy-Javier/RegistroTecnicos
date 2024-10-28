using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Components;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using RegistroTecnicos.Services;

namespace RegistroTecnicos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Obtener la cadena de conexión
            var SqlConStr = builder.Configuration.GetConnectionString("SqlConStr");
            builder.Services.AddDbContext<Contexto>(options =>
                options.UseSqlServer(SqlConStr));

            // Inyectar los servicios existentes
            builder.Services.AddScoped<TecnicosService>();
            builder.Services.AddScoped<TiposTecnicosService>();
            builder.Services.AddScoped<ClientesService>();
            builder.Services.AddScoped<TrabajosService>();
            builder.Services.AddScoped<PrioridadesService>();
            builder.Services.AddScoped<ArticuloService>();
            builder.Services.AddScoped<CotizacionesService>();

            // Inyectar BlazorBootstrap
            builder.Services.AddBlazorBootstrap();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}