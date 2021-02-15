using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewAsignacionesSaldo
    {
        public int IdAsignacion { get; set; }
        public int IdLote { get; set; }
        public string NombreProyecto { get; set; }
        [StringLength(81)]
        public string NombreLote { get; set; }
        [Required]
        public string NombreCompleto { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? MontoTotal { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaInicioPago { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? CuotaMinima { get; set; }
        public string Estado { get; set; }
        public bool? Donado { get; set; }
        [Column(TypeName = "numeric(38, 2)")]
        public decimal Abonado { get; set; }
        [Column(TypeName = "numeric(38, 2)")]
        public decimal? Saldo { get; set; }
        public int IdBeneficiario { get; set; }
        public bool? AplicaInteres { get; set; }
        public bool? PrimaCancelada { get; set; }
    public string Observaciones { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Prima { get; set; }
        [Required]
        [StringLength(150)]
        public string Municipio { get; set; }
        [Required]
        [StringLength(150)]
        public string Departamento { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal TasaInteres { get; set; }
        public bool AplicaMora { get; set; }
        public int Plazo { get; set; }
    }
}
