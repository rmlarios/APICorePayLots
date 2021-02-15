using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewAsignacionesLotes
    {
        public int IdAsignacion { get; set; }
        public int IdLote { get; set; }
        [StringLength(81)]
        public string NombreLote { get; set; }
        public int IdBeneficiario { get; set; }
        [Required]
        public string NombreCompleto { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaInicioPago { get; set; }
        public int? DiaCuota { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? MontoTotal { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? CuotaMinima { get; set; }
        public string Estado { get; set; }
        public bool? Donado { get; set; }
        public string CedulaIdentidad { get; set; }
        public string NombreProyecto { get; set; }
        public bool? AplicaInteres { get; set; }
        public bool? PrimaCancelada { get; set; }
        public string Observaciones { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Prima { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Area { get; set; }
        [Required]
        [StringLength(150)]
        public string Municipio { get; set; }
        [Required]
        [StringLength(150)]
        public string Departamento { get; set; }
        public string Identidad { get; set; }
        public string Telefono { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal TasaInteres { get; set; }
        public bool AplicaMora { get; set; }
        public int Plazo { get; set; }
        public int IdUbicacion { get; set; }
        public string Grupo { get; set; }
    }
}
