using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public double Sueldohora { get; set; } 


    [Required(ErrorMessage = "El Campo Descripci&oacute;n es obligatorio")]
        public string? Descripcion { get; set; }
    }
}
