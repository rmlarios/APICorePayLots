using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Dapper.Core.Entities
{
    public class Asignaciones
    {
        public Asignaciones()
        {
             Pagos = new Collection<Pagos>();
        }

        [Key]
        public int IdAsignacion { get; set; }
        [Display(Name ="Lote")]
        public int IdLote { get; set; }
        public int IdBeneficiario { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaInicioPago { get; set; }
        public int? DiaCuota { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? MontoTotal { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? CuotaMinima { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Prima { get; set; }
        public string Estado { get; set; }
        public bool? Donado { get; set; }
        public bool? AplicaInteres { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? TasaInteres { get; set; }
        public bool? AplicaMora { get; set; }
        public int? Plazo { get; set; }
        public string Observaciones { get; set; }

        [ForeignKey("IdBeneficiario")]
        [InverseProperty("Asignaciones")]
        public Beneficiarios IdBeneficiarioNavigation { get; set; }
        [ForeignKey("IdLote")]
        [InverseProperty("Asignaciones")]
        public Lotes IdLoteNavigation { get; set; }
        [InverseProperty("IdAsignacionNavigation")]
        public ICollection<Pagos> Pagos { get; set; }
    }
}
