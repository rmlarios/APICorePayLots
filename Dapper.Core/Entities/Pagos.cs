using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Entities
{
    public class Pagos
    {
         [Key]
        public int IdPago { get; set; }
        public int IdAsignacion { get; set; }
        public int? NumeroAbono { get; set; }
        public string NumeroRecibo { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRecibo { get; set; }
        [Required]
        public string MesPagado { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal MontoPago { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Mora { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Interés { get; set; }
        [StringLength(50)]
        public string Moneda { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? TasaCambio { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? MontoEfectivo { get; set; }
        public string Observaciones { get; set; }
                

        [ForeignKey("IdAsignacion")]
        [InverseProperty("Pagos")]
        public Asignaciones IdAsignacionNavigation { get; set; }
    }
}
