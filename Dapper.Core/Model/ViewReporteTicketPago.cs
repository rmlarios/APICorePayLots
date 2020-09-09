using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewReporteTicketPago
    {
        public string NumeroRecibo { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRecibo { get; set; }
        public string NumeroLote { get; set; }
        public int? NumeroAbono { get; set; }
        public string Beneficiario { get; set; }
        public string Proyecto { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Interes { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Mora { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Saldo { get; set; }
        [Required]
        public string Cajero { get; set; }
        [Required]
        public string MesPagado { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? SaldoInicial { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal MontoPago { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Prima { get; set; }
        public int? IdAsignacion { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? TotalPagar { get; set; }
    }
}
