using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPFinal_PNT1.Models
{
    public class Profesional : Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ICollection<Turno> TurnosAsignados { get; set; }
        public ICollection<Tratamiento> TratamientosAsignados { get; set; }
    }
}
