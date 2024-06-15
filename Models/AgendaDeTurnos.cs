using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TPFinal_PNT1.Models

{
    public class AgendaDeTurnos
    {
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
