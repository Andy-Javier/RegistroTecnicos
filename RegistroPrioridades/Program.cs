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

            //Obtenemos el ConStr para usarlo en el contexto
            var ConStr = builder.Configuration.GetConnectionString("ConStr");

            // Agregamos el contexto al builder con el ConStr
            builder.Services.AddDbContext<Contexto>(Options => Options.UseSqlite(ConStr));

            // Inyectar los servicios existentes
            builder.Services.AddScoped<TecnicosServices>();
            builder.Services.AddScoped<TiposTecnicosServices>();
            builder.Services.AddScoped<ClientesServices>();
            builder.Services.AddScoped<TrabajosServices>();
            builder.Services.AddScoped<PrioridadesServices>();
            builder.Services.AddScoped<ArticuloServices>();  

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
