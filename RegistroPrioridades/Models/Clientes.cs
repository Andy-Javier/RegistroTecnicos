using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        [Required]
        [StringLength(100)]
        public string? Nombres { get; set; }
        [Required(ErrorMessage = "El número es requerido")]
        public string? WhatsApp { get; set; }
    }

}
