using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFinal_PNT1.Context;
using TPFinal_PNT1.Models;
using Microsoft.Extensions.Logging;


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

    // Get para obtener vista de turno
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

    // Acción GET para mostrar el formulario de registrar tratamiento
    [HttpGet]
    public IActionResult RegistrarTratamiento()
    {
        ViewData["PacienteId"] = new SelectList(_context.Pacientes.ToList(), "Id", "NombreCompleto");
        ViewData["ProfesionalId"] = new SelectList(_context.Profesionales.ToList(), "Id", "NombreCompleto");
        return View();
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


    // Acción POST para procesar el formulario de registrar tratamiento
    [HttpPost]
    public IActionResult RegistrarTratamiento([Bind("Fecha,Tipo,PacienteId,ProfesionalId")] Tratamiento tratamiento)
    {
        // Excluir las propiedades de navegación del ModelState
        ModelState.Remove("Paciente");
        ModelState.Remove("Profesional");

        if (ModelState.IsValid)
        {
            var paciente = _context.Pacientes.Find(tratamiento.PacienteId);
            var profesional = _context.Profesionales.Find(tratamiento.ProfesionalId);

            if (paciente == null || profesional == null)
            {
                ModelState.AddModelError(string.Empty, "Paciente o Profesional no encontrado.");
                ViewData["PacienteId"] = new SelectList(_context.Pacientes.ToList(), "Id", "NombreCompleto", tratamiento.PacienteId);
                ViewData["ProfesionalId"] = new SelectList(_context.Profesionales.ToList(), "Id", "NombreCompleto", tratamiento.ProfesionalId);
                return View(tratamiento);
            }

            tratamiento.Paciente = paciente;
            tratamiento.Profesional = profesional;

            _context.Tratamientos.Add(tratamiento);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errors)
        {
            _logger.LogError(error.ErrorMessage);
        }

        ViewData["PacienteId"] = new SelectList(_context.Pacientes.ToList(), "Id", "NombreCompleto", tratamiento.PacienteId);
        ViewData["ProfesionalId"] = new SelectList(_context.Profesionales.ToList(), "Id", "NombreCompleto", tratamiento.ProfesionalId);
        return View(tratamiento);
    }

}