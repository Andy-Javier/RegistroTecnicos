using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class TiposTecnicos
    {
        [Key]
        public int TipoTecnicoId { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage = "Solo son permitidas las letras")]
        public string? Descripcion { get; set; }

        public ICollection<Tecnicos>? Tecnicos { get; set; } // Propiedad de navegación
    }
}
