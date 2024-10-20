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

        [Required(ErrorMessage = "El Cliente es obligatorio.")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Clientes? Cliente { get; set; }

        [Required(ErrorMessage = "El Técnico es obligatorio.")]
        public int TecnicoId { get; set; }

        [ForeignKey("TecnicoId")]
        public Tecnicos? Tecnico { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 0.")]
        public decimal Monto { get; set; }

        [ForeignKey("Prioridades")]
        [Required(ErrorMessage = "La prioridad es obligatoria.")]
        public int PrioridadId { get; set; }
        public Prioridades? Prioridades { get; set; }

        public ICollection<TrabajosDetalle>? TrabajosDetalle { get; set; } = new List<TrabajosDetalle>();
    }
}
