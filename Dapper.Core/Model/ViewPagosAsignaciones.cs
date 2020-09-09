using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewPagosAsignaciones
    {
        public int? IdPago { get; set; }
        public int IdAsignacion { get; set; }
        [StringLength(81)]
        public string NombreLote { get; set; }
        [Required]
        public string NombreCompleto { get; set; }
        [Required]
        public string NumeroRecibo { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRecibo { get; set; }
        public string MesPagado { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? MontoPago { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Mora { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Interés { get; set; }
        [Column(TypeName = "numeric(20, 2)")]
        public decimal? TotalRecibo { get; set; }
        public int IdLote { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? MontoTotal { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Prima { get; set; }
        [Column(TypeName = "numeric(38, 2)")]
        public decimal Abonado { get; set; }
        [Column(TypeName = "numeric(38, 2)")]
        public decimal? Saldo { get; set; }
        public int? NumeroAbono { get; set; }
        public string Estado { get; set; }
        [Required]
        public string Observaciones { get; set; }
        public string NombreProyecto { get; set; }
        public int IdUbicacion { get; set; }
        public string Grupo { get; set; }
        public bool? Donado { get; set; }
        [StringLength(50)]
        public string Moneda { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? TasaCambio { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? MontoEfectivo { get; set; }
    }
}
