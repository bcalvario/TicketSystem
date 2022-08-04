using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Titulo { get; set; }

        [Required]
        [MaxLength(256)]
        public string Descripcion { get; set; }

        [Required]
        [MaxLength(60)]
        public string Estatus { get; set; }

        [Required]
        [MaxLength(60)]
        public string EncargadoNombre { get; set; }

        [MaxLength(60)]
        public string EncargadoApellido1 { get; set; }

        [MaxLength(60)]
        public string EncargadoApellido2 { get; set; }

        [Required]
        [MaxLength(60)]
        public string EncargadoCorreo { get; set; }

    }

    public class kdddd
    {

    }
}
