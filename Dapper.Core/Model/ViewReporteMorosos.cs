using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewReporteMorosos
    {
        public int IdAsignacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaInicioPago { get; set; }
        [Required]
        public string Beneficiario { get; set; }
        public string Telefono { get; set; }
        [StringLength(81)]
        public string NombreLote { get; set; }
        public string Ubicacion { get; set; }
        public int? Cuotas { get; set; }
        public int? CuotasRequeridas { get; set; }
    }
}
