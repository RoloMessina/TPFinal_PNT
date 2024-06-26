using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFinal_PNT1.Context;
using TPFinal_PNT1.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

public class AgendaDeTurnosController : Controller
{
    private readonly AgendaDeTurnos _agendaDeTurnos;
    private readonly AgendaContext _context;
    private readonly ILogger<AgendaDeTurnosController> _logger;

    public AgendaDeTurnosController(AgendaDeTurnos agendaDeTurnos, AgendaContext context, ILogger<AgendaDeTurnosController> logger)
    {
        _agendaDeTurnos = agendaDeTurnos;
        _context = context;
        _logger = logger;
    }

    public IActionResult AsignarTurno()
    {
        ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto");
        ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "NombreCompleto");
        return View();
    }

    [HttpPost]
    public IActionResult AsignarTurno(int pacienteId, int profesionalId, DateTime fecha)
    {
        var paciente = _context.Pacientes.Find(pacienteId);
        var profesional = _context.Profesionales.Find(profesionalId);

        if (paciente == null || profesional == null)
        {
            ModelState.AddModelError(string.Empty, "Paciente o Profesional no encontrado.");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto", pacienteId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "NombreCompleto", profesionalId);
            return View();
        }

        if (fecha < DateTime.Today)
        {
            ModelState.AddModelError(string.Empty, "La fecha del turno no puede ser menor al día actual.");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto", pacienteId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "NombreCompleto", profesionalId);
            return View();
        }

        bool existeTurno = _context.Turnos.Any(t => t.Fecha == fecha && t.ProfesionalId == profesionalId);
        if (existeTurno)
        {
            ModelState.AddModelError(string.Empty, "Ya existe un turno asignado para ese horario y profesional. Por favor, elige otra fecha u hora.");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto", pacienteId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "NombreCompleto", profesionalId);
            return View();
        }

        var exito = _agendaDeTurnos.AsignarTurno(paciente.Id, profesional.Id, fecha);

        if (exito)
        {
            return RedirectToAction("ListarTurnosAsignados");
        }

        ModelState.AddModelError(string.Empty, "No se pudo asignar el turno.");
        ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto", pacienteId);
        ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "NombreCompleto", profesionalId);
        return View();
    }

    public IActionResult ListarTurnosAsignados()
    {
        var turnos = _agendaDeTurnos.ListarTurnosAsignados();
        return View(turnos);
    }

    [HttpPost]
    public IActionResult CancelarTurno(int id)
    {
        var turno = _context.Turnos.Find(id);
        if (turno == null)
        {
            return NotFound();
        }

        _agendaDeTurnos.CancelarTurno(turno);
        return RedirectToAction("ListarTurnosAsignados");
    }

    public IActionResult EditarTurno(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var turno = _context.Turnos
            .Include(t => t.Paciente)
            .Include(t => t.Profesional)
            .FirstOrDefault(t => t.Id == id);
        if (turno == null)
        {
            return NotFound();
        }

        ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto", turno.PacienteId);
        ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "NombreCompleto", turno.ProfesionalId);
        return View(turno);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditarTurno(int id, [Bind("Id,PacienteId,ProfesionalId,Fecha")] Turno turno)
    {
        if (id != turno.Id)
        {
            _logger.LogError("El Id del turno ({TurnoId}) no coincide con el Id de la URL ({Id}).", turno.Id, id);
            return NotFound();
        }
        // Excluir las propiedades de navegación del ModelState
        ModelState.Remove("Paciente");
        ModelState.Remove("Profesional");

        if (ModelState.IsValid)
        {
            try
            {
                _agendaDeTurnos.ModificarTurno(turno);
                return RedirectToAction(nameof(ListarTurnosAsignados));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!_context.Turnos.Any(e => e.Id == turno.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto", turno.PacienteId);
        ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "NombreCompleto", turno.ProfesionalId);
        return View(turno);
    }
}