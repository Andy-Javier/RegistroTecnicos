using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{
    public class TrabajosDetalle
    {
        [Key]
        public int? DetalleId { get; set; }

        [ForeignKey("TrabajosId")]
        public Trabajos? Trabajos { get; set; }

        [Required(ErrorMessage = "El Trabajo es obligatorio.")]
        public int? TrabajosId { get; set; }

        [ForeignKey("ArticuloId")]
        public Articulos? Articulos { get; set; }

        [Required(ErrorMessage = "El Artículo es obligatorio.")]
        public int? ArticuloId { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        public decimal? Precio { get; set; }

        [Required(ErrorMessage = "El costo es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor que 0.")]
        public decimal? Costo { get; set; }
    }
}
