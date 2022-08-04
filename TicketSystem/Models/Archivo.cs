using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Models
{
    public class Archivo
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public IFormFile archivo { get; set; }

        public string text { get; set; }

    }
}
