using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFinal_PNT1.Context;
using TPFinal_PNT1.Models;

namespace TPFinal_PNT1.Controllers
{
    public class PacienteController : Controller
    {
        private readonly AgendaContext _context;

        public PacienteController(AgendaContext context)
        {
            _context = context;
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
            if (ModelState.IsValid)
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
                }
            }
            return View(paciente);
        }

        // Acción GET para listar todos los pacientes
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pacientes = await _context.Pacientes.ToListAsync();
            return View(pacientes);
        }
    }
}