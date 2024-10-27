using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Cotizaciones
    {
        [Key]
        public int CotizacionId { get; set; }
        public DateTime? Fecha { get; set; }
        public int ClienteId { get; set; }
        public string? Observacion { get; set; }
        public decimal Monto { get; set; }
        public List<CotizacionesDetalle> CotizacionesDetalle { get; set; } = new List<CotizacionesDetalle>();
    }
}
