using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TPFinal_PNT1.Context;

namespace TPFinal_PNT1.Models

{
    public class AgendaDeTurnos : ITurnera
    {
        private readonly AgendaContext _context;
        private readonly ILogger<AgendaDeTurnos> _logger;


        public AgendaDeTurnos(AgendaContext context, ILogger<AgendaDeTurnos> logger)
        {
            _context = context;
            _logger = logger;
        }


        public bool AsignarTurno(int pacienteId, int profesionalId, DateTime fecha)
        {
            var turno = new Turno
            {
                PacienteId = pacienteId,
                ProfesionalId = profesionalId,
                Fecha = fecha
            };

            _context.Turnos.Add(turno);

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al guardar el turno: {ex.Message}");
                return false;
            }
        }

        public List<Turno> ListarTurnosAsignados()
        {
            return _context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Profesional)
                .ToList();
        }

        public bool AltaUsuario(Usuario usuario)
        {
            // Lógica para dar de alta un usuario
            return true;
        }

        public void AgregarTurno(Turno turno)
        {
            // Lógica para agregar un turno
            _context.Turnos.Add(turno);
            _context.SaveChanges();
        }

        public void CancelarTurno(Turno turno)
        {
            // Lógica para cancelar un turno
            _context.Turnos.Remove(turno);
            _context.SaveChanges();
        }

        public void ModificarTurno(Turno turno)
        {
            // Lógica para modificar un turno
            _context.Turnos.Update(turno);
            _context.SaveChanges();
        }

        public bool RegistrarTratamiento(Paciente paciente, Profesional profesional, Tratamiento tratamiento, DateTime fecha)
        {
            tratamiento.Fecha = fecha;
            tratamiento.Paciente = paciente;
            tratamiento.Profesional = profesional;

            _context.Tratamientos.Add(tratamiento);

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al guardar el tratamiento: {ex.Message}");
                return false;
            }
        }

        public void VisualizarTurno(Turno turno)
        {
            throw new NotImplementedException();
        }

    }
}
