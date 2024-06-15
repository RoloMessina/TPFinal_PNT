using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPFinal_PNT1.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreCompleto { get; set; }

        [Required]
        [MaxLength(20)]
        public string DNI { get; set; }

        [Required]
        [MaxLength(200)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        public ICollection<Turno> Turnos { get; set; }
    }
}
