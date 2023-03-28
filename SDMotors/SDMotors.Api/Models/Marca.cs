using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDMotors.Api.Models
{
    public class Marca
    {
        [Key]
        public int MarcaId { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaRegistro { get; set; } = DateTime.Now;
    }
    public class MarcaDto
    {
        public string? Nombre { get; set; }
    }
}
