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

        public AgendaDeTurnos(AgendaContext context)
        {
            _context = context;
        }

        public bool AsignarTurno(Usuario usuario, Fecha fecha)
        {
            // Lógica para asignar turno
            return true;
        }

        public List<Turno> ListarTurnosAsignados()
        {
            // Lógica para listar turnos asignados
            return new List<Turno>();
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

        public void VisualizarTurno(Turno turno)
        {
            // Lógica para visualizar un turno
        }

        public bool AsignarTurno(Usuario usuario, DateTime fecha)
        {
            // Lógica para asignar un turno a un usuario
            return true;
        }

        public bool RegistrarTratamiento(Paciente paciente, Profesional profesional, Tratamiento tratamiento, DateTime fecha)
        {
            // Lógica para registrar un tratamiento
            return true;
        }

        public void VerTratamientosAsignados(Profesional profesional)
        {
            // Lógica para ver tratamientos asignados por un profesional
        }

        //public bool RegistrarTratamiento(Paciente paciente, Profesional profesional, Tratamiento tratamiento, Fecha fecha)
        //{
        // Lógica para registrar tratamiento
        //  return true;
        //}

        //public Mensaje NotificarCambioTurno(Paciente paciente, Turno turno)
        //{
        //  // Lógica para notificar cambio de turno
        //return new Mensaje();
        //}

        //public Mensaje RecordatorioTurno(Profesional profesional, Paciente paciente, Turno turno)
        //{
        // Lógica para enviar recordatorio de turno
        //  return new Mensaje();
        //}

        //public void VerTratamientosAsignados(Profesional profesional)
        //{
        // Lógica para ver tratamientos asignados
        //}
    }
}
