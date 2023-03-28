using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDMotors.Api.Models
{
    public enum Status
    {
        Nuevo,
        Usado
    }
    public class Vehiculo
    {
        [Key]
        public Guid VehiculoId { get; set; } = Guid.NewGuid();  
        public string? Modelo { get; set; }
        public Status? Status { get; set; }
        [NotMapped]
        public string? StatusNombre { get; set; }
        [ForeignKey("MarcaId")]
        public int MarcaId { get; set; }
        public Marca? Marca { get; set; } 

        [MaxLength(4)]
        public int? Año { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Precio { get; set; } 
        public string? ImagenUrl { get; set; }
        public DateTime? FechaRegistro { get; set; } = DateTime.Now;
    }
    public class VehiculoDto
    {
        public string? Modelo { set; get; }
        public Status? Status { get; set; }
        public int MarcaId { set; get; }
        public int? Año { set; get; }
        public decimal? Precio { get; set; }
        public string? ImagenUrl { get; set; }
    }
}
