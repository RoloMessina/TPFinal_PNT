using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TPFinal_PNT1.Context;
using TPFinal_PNT1.Models;
using System.Linq;

namespace TPFinal_PNT1.Controllers
{
    public class PacienteController : Controller
    {
        private readonly AgendaContext _context;
        private readonly ILogger<PacienteController> _logger;

        public PacienteController(AgendaContext context, ILogger<PacienteController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Acción GET para mostrar el formulario de crear paciente
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Acción POST para procesar el formulario de crear paciente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NombreCompleto,DNI,PasswordHash,Email")] Paciente paciente)
        {

            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
            /*if (ModelState.IsValid)
            {
                try
                {
                    _context.Pacientes.Add(paciente);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error al guardar el paciente: {ex.Message}");
                    _logger.LogError(ex, "Error al guardar el paciente.");
                }
            }
            else
            {
                // Log de errores de validación
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    _logger.LogError(error.ErrorMessage); // Registrar el error en el log
                }
            }*/
            return View(paciente);
        }
    }
}
