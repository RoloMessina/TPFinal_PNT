using System.Collections.Generic;

namespace TPFinal_PNT1.Models
{
    public class Paciente : Usuario
    {
        public ICollection<Turno> Turnos { get; set; }
        public ICollection<Tratamiento> Tratamientos { get; set; }
    }
}
