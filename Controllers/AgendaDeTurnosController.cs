using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFinal_PNT1.Context;
using TPFinal_PNT1.Models;

public class AgendaDeTurnosController : Controller
{
    private readonly AgendaDeTurnos _agendaDeTurnos;
    private readonly AgendaContext _context;


    public AgendaDeTurnosController(AgendaDeTurnos agendaDeTurnos, AgendaContext context)
    {
        _agendaDeTurnos = agendaDeTurnos;
        _context = context;
    }

/*    // Get para obtener vista de turno
    public IActionResult AsignarTurno()
    {
        ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Nombre");
        ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "Nombre");
        return View();
    }


    [HttpPost]
    public IActionResult AsignarTurno(Usuario usuario, DateTime fecha)
    {
        var exito = _agendaDeTurnos.AsignarTurno(usuario, fecha);
        return Json(new { success = exito });
    }

    public IActionResult ListarTurnosAsignados()
    {
        var turnos = _agendaDeTurnos.ListarTurnosAsignados();
        return View(turnos);
    }*/

    // Acción GET para mostrar el formulario de registrar tratamiento
    [HttpGet]
    public IActionResult RegistrarTratamiento()
    {
        ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto");
        ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "NombreCompleto");
        return View();
    }

    // Acción POST para procesar el formulario de registrar tratamiento
    [HttpPost]
    public IActionResult RegistrarTratamiento(Tratamiento tratamiento)
    {
        if (ModelState.IsValid)
        {
            _context.Tratamientos.Add(tratamiento);
            _context.SaveChanges();
            return RedirectToAction("Index"); // Redirigir a una acción apropiada después de registrar el tratamiento
        }

        ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto", tratamiento.PacienteId);
        ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "NombreCompleto", tratamiento.ProfesionalId);
        return View(tratamiento);
    }


    // Acción GET para mostrar tratamientos asignados a un profesional
    public IActionResult VerTratamientosAsignados()
    {
        var tratamientos = _context.Tratamientos
            .Include(t => t.Paciente)
            .Include(t => t.Profesional)
            .ToList();

        return View(tratamientos);
    }
}
