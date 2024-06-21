using Microsoft.EntityFrameworkCore;
using TPFinal_PNT1.Context;
using TPFinal_PNT1.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddScoped<AgendaDeTurnos>();
builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Otros servicios
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuración de middleware y otros
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "asignarTurno",
    pattern: "{controller=AgendaDeTurnos}/{action=AsignarTurno}");

app.MapControllerRoute(
    name: "listarTurnosAsignados",
    pattern: "{controller=AgendaDeTurnos}/{action=ListarTurnosAsignados}");

app.MapControllerRoute(
    name: "registrarTratamiento",
    pattern: "{controller=AgendaDeTurnos}/{action=RegistrarTratamiento}");

app.MapControllerRoute(
    name: "paciente",
    pattern: "{controller=Paciente}/{action=Create}");

app.MapControllerRoute(
    name: "listarPacientes",
    pattern: "{controller=Paciente}/{action=Index}");

app.MapControllerRoute(
    name: "crearProfesional",
    pattern: "{controller=Profesional}/{action=Create}");

app.Run();
