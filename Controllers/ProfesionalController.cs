using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TPFinal_PNT1.Context;
using TPFinal_PNT1.Models;
using System.Linq;

namespace TPFinal_PNT1.Controllers
{
    public class ProfesionalController : Controller
    {
        private readonly AgendaContext _context;
        private readonly ILogger<ProfesionalController> _logger;

        public ProfesionalController(AgendaContext context, ILogger<ProfesionalController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Acción GET para mostrar el formulario de crear profesional
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Acción POST para procesar el formulario de crear profesional
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NombreCompleto,DNI,PasswordHash,Email")] Profesional profesional)
        {
            _context.Profesionales.Add(profesional);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
            /*if (ModelState.IsValid)
            {
                try
                {
                    _context.Profesionales.Add(profesional);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error al guardar el profesional: {ex.Message}");
                    _logger.LogError(ex, "Error al guardar el profesional.");
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
            return View(profesional);
        }

        // Acción GET para listar los profesionales
        [HttpGet]
        public IActionResult Index()
        {
            var profesionales = _context.Profesionales.ToList();
            return View(profesionales);
        }
    }
}
