using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
public class Tecnicos
{
    [Key]
        public int TecnicosId { get; set; }
        [Required(ErrorMessage = "El Campo Descripci&oacute;n es obligatorio")]
        public string? Descripcion { get; set; }
    }
}
