using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewReportePlanPago
    {
        public int? IdAsignacion { get; set; }
        public string Proyecto { get; set; }
        public string NumeroLote { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? AreaLote { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Total { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Prima { get; set; }
        public string Beneficiario { get; set; }
        public int? NumeroCuota { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaCuota { get; set; }
        public string MesPagado { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? SaldoInicial { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? MontoMinimo { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Saldo { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Interes { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? TotalPagar { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? MontoPagado { get; set; }
        public string FechaPago { get; set; }
        public string Estado { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Mora { get; set; }
    }
}
