using System.ComponentModel.DataAnnotations;

namespace TPFinal_PNT1.Models
{
    public abstract class Usuario
    {
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
    }
}
