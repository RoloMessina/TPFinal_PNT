using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TPFinal_PNT1.Context;
using TPFinal_PNT1.Models;

namespace TPFinal_PNT1.Services
{
    public class GestorDeTratamientos
    {
        private readonly AgendaContext _context;
        private readonly ILogger<GestorDeTratamientos> _logger;

        public GestorDeTratamientos(AgendaContext context, ILogger<GestorDeTratamientos> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> RegistrarTratamiento(Tratamiento tratamiento)
        {
            var paciente = await _context.Pacientes.FindAsync(tratamiento.PacienteId);
            var profesional = await _context.Profesionales.FindAsync(tratamiento.ProfesionalId);

            if (paciente == null || profesional == null)
            {
                _logger.LogError("Paciente o Profesional no encontrado.");
                return false;
            }

            tratamiento.Paciente = paciente;
            tratamiento.Profesional = profesional;

            _context.Tratamientos.Add(tratamiento);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al guardar el tratamiento: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Tratamiento>> ObtenerTratamientos()
        {
            return await _context.Tratamientos
                .Include(t => t.Paciente)
                .Include(t => t.Profesional)
                .ToListAsync();
        }

        public async Task<bool> CancelarTratamiento(Tratamiento t)
        {
            _context.Tratamientos.Remove(t);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarTratamiento(Tratamiento tratamiento)
        {
            _context.Update(tratamiento);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TratamientoExists(tratamiento.Id))
                {
                    _logger.LogError("Tratamiento no encontrado.");
                    return false;
                }
                else
                {
                    _logger.LogError($"Error de concurrencia al actualizar el tratamiento: {ex.Message}");
                    throw;
                }
            }
        }

        private bool TratamientoExists(int id)
        {
            return _context.Tratamientos.Any(e => e.Id == id);
        }
    }
}
