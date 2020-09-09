using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewDashboard1
    {
        public string NombreProyecto { get; set; }
        [Required]
        [StringLength(50)]
        public string Bloque { get; set; }
        [Column(TypeName = "numeric(20, 2)")]
        public decimal? Pagado { get; set; }
        [Required]
        public string MesPagado { get; set; }
        public string Estado { get; set; }
        public int IdLote { get; set; }
        [Column("FAR", TypeName = "smalldatetime")]
        public DateTime? Far { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? MontoTotal { get; set; }
        [Column(TypeName = "numeric(38, 2)")]
        public decimal? Saldo { get; set; }
        public string NombreCompleto { get; set; }
        [Required]
        public string NombreLote { get; set; }
        public string EstadoAsignacion { get; set; }
        public int IdUbicacion { get; set; }
    }
}
