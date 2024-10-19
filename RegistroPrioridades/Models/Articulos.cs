using System.ComponentModel.DataAnnotations;
namespace RegistroTecnicos.Models;

public class Articulos
{
    [Key]
    public int ArticuloId { get; set; }

    [Required(ErrorMessage = "La descripción es requerida.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "La descripción solo puede contener letras, números y espacios.")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El costo es requerido.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor que 0.")]
    public decimal Costo { get; set; }

    [Required(ErrorMessage = "El precio es requerido.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "La existencia es requerida.")]
    [Range(0, int.MaxValue, ErrorMessage = "La existencia debe ser un número mayor o igual a 0.")]
    public int Existencia { get; set; }
}

