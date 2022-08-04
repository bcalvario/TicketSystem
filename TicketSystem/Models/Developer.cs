using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models
{
    public class Developer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        [MinLength(2)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(60)]
        [MinLength(2)]
        public string Apellido1 { get; set; }

        [MaxLength(60)]
        [MinLength(2)]
        public string Apellido2 { get; set; }

        [Required]
        [MaxLength(60)]
        [MinLength(2)]
        public string Puesto { get; set; }

        [Required]
        [MaxLength(60)]
        [MinLength(2)]
        public string Correo { get; set; }
    }
}
