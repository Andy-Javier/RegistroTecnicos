using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Services;
using RegistroTecnicos.Components;
using RegistroTecnicos.DAL;

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
            var ConStr = builder.Configuration.GetConnectionString("ConStr");

            // Agregar el contexto al builder con el ConStr
            builder.Services.AddDbContext<Contexto>(Options => Options.UseSqlite(ConStr));

            // Registrar ContextoFactory
            builder.Services.AddSingleton<ContextoFactory>();

            // Inyectar los servicios existentes
            builder.Services.AddScoped<TecnicosService>();
            builder.Services.AddScoped<TiposTecnicosService>();
            builder.Services.AddScoped<ClientesService>();
            builder.Services.AddScoped<TrabajosService>();
            builder.Services.AddScoped<PrioridadesService>();
            builder.Services.AddScoped<ArticuloService>();
            builder.Services.AddScoped<CotizacionesService>();
            builder.Services.AddScoped<CotizacionesDetalleService>();

            // Inyectar BlazorBootstrap
            builder.Services.AddBlazorBootstrap();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
