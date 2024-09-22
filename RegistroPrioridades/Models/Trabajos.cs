using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RegistroTecnicos.Models
{
    public class Trabajos
    {
        [Key]
        public int TrabajoId { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public int TecnicoId { get; set; }
        public string? Descripcion { get; set; }
        [Required]
        public decimal Monto { get; set; }
        [ForeignKey("ClienteId")]
        public Clientes? Cliente { get; set; }
        public Tecnicos? Tecnico { get; set; }
    }
}