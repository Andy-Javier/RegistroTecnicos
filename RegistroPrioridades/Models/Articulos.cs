using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Articulos
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "El Id debe de ser mayor a 1")]
        public int ArticuloId { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El costo es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor que 0.")]
        public decimal? Costo { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        public decimal? Precio { get; set; }

        [Required(ErrorMessage = "La existencia es obligatoria.")]
        [Range(0, double.MaxValue, ErrorMessage = "La existencia no puede ser negativa.")]
        public decimal? Existencia { get; set; }
    }
}
