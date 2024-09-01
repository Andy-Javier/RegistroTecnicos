using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Services; 
using RegistroTecnicos.DAL;
using RegistroTecnicos.Components;

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

            //Agregamos el contexto al builder con el ConStr
            builder.Services.AddDbContext<Contexto>(Options => Options.UseSqlite(ConStr));

            builder.Services.AddScoped<TecnicosServices>();

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
