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

    // Get para obtener vista de turno
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
    }

    [HttpPost]
    public IActionResult RegistrarTratamiento(Paciente paciente, Profesional profesional, Tratamiento tratamiento, DateTime fecha)
    {
        var exito = _agendaDeTurnos.RegistrarTratamiento(paciente, profesional, tratamiento, fecha);
        return Json(new { success = exito });
    }

    public IActionResult VerTratamientosAsignados(Profesional profesional)
    {
        _agendaDeTurnos.VerTratamientosAsignados(profesional);
        return View();
    }
}
