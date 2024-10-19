using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{
    public class TrabajosDetalle
    {
        [Key]
        public int DetalleId { get; set; }

        [Required(ErrorMessage = "El TrabajoId es requerido.")]
        public int TrabajoId { get; set; }

        [ForeignKey("TrabajoId")]
        public Trabajos? Trabajo { get; set; } // Relación con Trabajos

        [Required(ErrorMessage = "El ArticuloId es requerido.")]
        public int ArticuloId { get; set; }

        [ForeignKey("ArticuloId")]
        public Articulos? Articulo { get; set; } // Relación con Articulos

        [Required(ErrorMessage = "La cantidad es requerida.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El costo es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor que 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Costo { get; set; }
    }
}
