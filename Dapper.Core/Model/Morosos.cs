using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class Morosos
    {
        public int? IdAsignacion { get; set; }
        public string Beneficiario { get; set; }
        public string Telefono { get; set; }
        public string NombreLote { get; set; }
        public string Ubicacion { get; set; }
        public int? CuotasVencidas { get; set; }
        public string MesesVencidos { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? MontoCuotasVencidas { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? InteresAcumulado { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? MoraAcumulada { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaGenerado { get; set; }
    }
}
