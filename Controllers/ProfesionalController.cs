using Microsoft.AspNetCore.Mvc;
using TPFinal_PNT1.Context;
using TPFinal_PNT1.Models;

namespace TPFinal_PNT1.Controllers
{
    public class ProfesionalController : Controller
    {
        private readonly AgendaContext _context;

        public ProfesionalController(AgendaContext context)
        {
            _context = context;
        }

        // GET: Profesional/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesional/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NombreCompleto,DNI,PasswordHash,Email")] Profesional profesional)
        {
            if (ModelState.IsValid)
            {
                _context.Profesionales.Add(profesional);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home"); // Cambia esto a donde quieras redirigir después de crear el profesional
            }
            return View(profesional);
        }
    }
}
