using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Prioridades
    {
        [Key]
        public int PrioridadId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Tiempo { get; set; }
    }

}
