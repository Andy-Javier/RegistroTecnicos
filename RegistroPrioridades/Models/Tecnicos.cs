using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SueldoHora { get; set; }

        [ForeignKey("TipoTecnico")]
        public int TipoTecnicoId { get; set; }

        public TiposTecnicos TipoTecnico { get; set; } // Propiedad de navegación
    }
}
