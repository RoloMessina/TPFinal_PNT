using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFinal_PNT1.Context;
using TPFinal_PNT1.Models;
using TPFinal_PNT1.Services;
using Microsoft.Extensions.Logging;

namespace TPFinal_PNT1.Controllers
{
    public class TratamientoController : Controller
    {
        private readonly GestorDeTratamientos _gestorDeTratamientos;
        private readonly ILogger<TratamientoController> _logger;
        private readonly AgendaContext _context;

        public TratamientoController(GestorDeTratamientos gestorDeTratamientos, AgendaContext context, ILogger<TratamientoController> logger)
        {
            _gestorDeTratamientos = gestorDeTratamientos;
            _context = context;
            _logger = logger;
        }

        // GET: Tratamiento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamiento = await _gestorDeTratamientos.ObtenerTratamientoPorId(id.Value);
            if (tratamiento == null)
            {
                return NotFound();
            }

            return View(tratamiento);
        }

        // Acción GET para mostrar el formulario de registrar tratamiento
        [HttpGet]
        public IActionResult RegistrarTratamiento()
        {
            ViewData["PacienteId"] = new SelectList(_context.Pacientes.ToList(), "Id", "NombreCompleto");
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales.ToList(), "Id", "NombreCompleto");
            return View();
        }

        // Acción POST para procesar el formulario de registrar tratamiento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarTratamiento([Bind("Fecha,Tipo,PacienteId,ProfesionalId,Descripcion")] Tratamiento tratamiento)
        {
            // Excluir las propiedades de navegación del ModelState
            ModelState.Remove("Paciente");
            ModelState.Remove("Profesional");

            if (ModelState.IsValid)
            {
                var result = await _gestorDeTratamientos.RegistrarTratamiento(tratamiento);
                if (result)
                {
                    return RedirectToAction("VerTratamientos");
                }
                ModelState.AddModelError(string.Empty, "Error al registrar el tratamiento.");
            }

            ViewData["PacienteId"] = new SelectList(_context.Pacientes.ToList(), "Id", "NombreCompleto", tratamiento.PacienteId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales.ToList(), "Id", "NombreCompleto", tratamiento.ProfesionalId);
            return View(tratamiento);
        }

        // GET: Tratamiento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamiento = await _gestorDeTratamientos.ObtenerTratamientoPorId(id.Value);
            if (tratamiento == null)
            {
                return NotFound();
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "DNI", tratamiento.PacienteId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "DNI", tratamiento.ProfesionalId);
            return View(tratamiento);
        }

        // POST: Tratamiento/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Tipo,PacienteId,ProfesionalId,Descripcion")] Tratamiento tratamiento)
        {
            if (id != tratamiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _gestorDeTratamientos.ActualizarTratamiento(tratamiento);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al actualizar el tratamiento.");
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "DNI", tratamiento.PacienteId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "DNI", tratamiento.ProfesionalId);
            return View(tratamiento);
        }

        // GET: Tratamiento/VerTratamientos
        public async Task<IActionResult> VerTratamientos()
        {
            var tratamientos = await _gestorDeTratamientos.ObtenerTratamientos();
            return View(tratamientos);
        }

        // GET: Tratamiento/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamiento = await _gestorDeTratamientos.ObtenerTratamientoPorId(id.Value);
            if (tratamiento == null)
            {
                return NotFound();
            }

            return View(tratamiento);
        }

        // POST: Tratamiento/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _gestorDeTratamientos.EliminarTratamiento(id);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Error al eliminar el tratamiento.");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TratamientoExists(int id)
        {
            return _gestorDeTratamientos.ObtenerTratamientoPorId(id) != null;
        }
    }
}
