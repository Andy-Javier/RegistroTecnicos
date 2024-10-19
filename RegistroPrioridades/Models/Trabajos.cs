using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{
    public class Trabajos
    {
        [Key]
        public int TrabajoId { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El técnico es obligatorio.")]
        public int TecnicoId { get; set; }

        [Required(ErrorMessage = "La prioridad es obligatoria.")]
        public int PrioridadId { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s,.'-]+$", ErrorMessage = "La descripción contiene caracteres no válidos.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        [ForeignKey("ClienteId")]
        public Clientes? Cliente { get; set; }

        [ForeignKey("TecnicoId")]
        public Tecnicos? Tecnico { get; set; }

        [ForeignKey("PrioridadId")]
        public Prioridades? Prioridad { get; set; }

        public ICollection<TrabajosDetalle>? TrabajosDetalles { get; set; }
    }
}
