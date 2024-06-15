using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPFinal_PNT1.Models
{
    public class Tratamiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [ForeignKey("Paciente")]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        [ForeignKey("Profesional")]
        public int ProfesionalId { get; set; }
        public Profesional Profesional { get; set; }
    }
}
