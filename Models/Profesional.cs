using System.Collections.Generic;

namespace TPFinal_PNT1.Models
{
    public class Profesional : Usuario
    {
        public ICollection<Turno> TurnosAsignados { get; set; }
        public ICollection<Tratamiento> TratamientosAsignados { get; set; }
    }
}
